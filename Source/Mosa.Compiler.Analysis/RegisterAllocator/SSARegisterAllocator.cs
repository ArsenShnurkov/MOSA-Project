/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis.RegisterAllocator
{
	public class SSARegisterAllocator
	{
		MethodBody body;
		IList<BasicBlock> blocks;
		LoopAwareBlockOrder blockOrder;

		BasicBlock[] instrParent;

		Value[] values;
		Dictionary<BasicBlock, BlockLiveness> liveness = new Dictionary<BasicBlock, BlockLiveness>();
		Dictionary<Value, LiveInterval> liveIntervals = new Dictionary<Value, LiveInterval>();

		public SSARegisterAllocator(MethodBody body)
		{
			this.body = body;
			blocks = body.EntryBlock.ReversePostOrder();
			blockOrder = new LoopAwareBlockOrder(blocks);

			uint seq = 0;
			List<BasicBlock> parents = new List<BasicBlock>();
			foreach (var block in blocks)
			{
				foreach (var instr in block.Body)
				{
					instr.Sequence = seq++;
					parents.Add(block);
				}
			}
			instrParent = parents.ToArray();
		}

		string VisualizeLiveIntervals()
		{
			SortedDictionary<string, string> intervals = new SortedDictionary<string, string>();

			StringBuilder line = new StringBuilder();
			line.Append("          |");
			for (uint i = 0; i < instrParent.Length; i++)
			{
				if (i % 10 == 0)
					line.Append((i / 10) % 10);
				else
					line.Append(" ");
			}
			string header = line.ToString();

			foreach (var interval in liveIntervals.Values)
			{
				line.Length = 0;
				line.AppendFormat("{0}|", interval.Value.ToString().PadLeft(10));
				for (uint i = 0; i < instrParent.Length; i++)
				{
					line.Append(interval.Contains(i) ? '=' : ' ');
				}
				intervals.Add(interval.Value.ToString(), line.ToString());
			}
			return header + "\r\n" + string.Join("\r\n", new List<string>(intervals.Values).ToArray());
		}

		string VisualizeBody()
		{
			SortedDictionary<uint, string> instrs = new SortedDictionary<uint, string>();

			foreach (var block in blocks)
				foreach (var instr in block.Body)
				{
					instrs.Add(instr.Sequence, string.Format("{0:d3} | {1}", instr.Sequence, instr.ToString()));
				}

			return string.Join("\r\n", new List<string>(instrs.Values).ToArray());
		}

		IEnumerable<Value> CollectValues()
		{
			foreach (var block in blocks)
				foreach (var instr in block.Body)
				{
					for (int i = 0; i < instr.Results.Count; i++)
						yield return instr.Results[i];

					for (int i = 0; i < instr.Operands.Count; i++)
					{
						var operand = instr.Operands[i];
						if (operand is ValueOperand)
							yield return ((ValueOperand)operand).Value;
					}
				}
		}

		void ComputeLocalLiveness()
		{
			// Initialization
			Dictionary<Value, int> indexes = new Dictionary<Value, int>();

			for (int i = 0; i < values.Length; i++)
			{
				var value = values[i];
				indexes[value] = i;
				liveIntervals[value] = new LiveInterval(value);
			}

			var reverseOrder = new List<BasicBlock>(blocks);
			reverseOrder.Reverse();

			// Loop through all blocks
			InstrPointer ptr = new InstrPointer();
			foreach (var block in reverseOrder)
			{
				BitArray gen = new BitArray(values.Length);
				BitArray kill = new BitArray(values.Length);
				Instruction actualBegin = null;

				ptr.SetBlock(block);
				do
				{
					var instr = ptr.Instruction;
					if (instr.OpCode == IROpCodes.Phi)
					{
						// Do not process operands for phi functions, as they are not really a usage (only a marker of flow convergence)
						int index = indexes[instr.Result];
						kill.Set(index, true);
						continue;
					}
					else if (actualBegin == null)
						actualBegin = instr;

					for (int i = 0; i < instr.Results.Count; i++)
					{
						var value = instr.Results[i];
						int index = indexes[value];
						kill.Set(index, true);
					}

					for (int i = 0; i < instr.Operands.Count; i++)
					{
						var operand = instr.Operands[i];
						if (operand is ValueOperand)
						{
							var value = ((ValueOperand)operand).Value;
							int index = indexes[value];
							if (!kill.Get(index))
								gen.Set(index, true);
						}
					}
				} while (ptr.MoveNext());

				liveness[block] = new BlockLiveness()
				{
					ActualBegin = actualBegin,
					Gen = gen,
					Kill = kill,
					KillNot = new BitArray(kill).Not()
				};
			}
		}

		void ComputeGlobalLiveness()
		{
			bool changed;
			do
			{
				changed = false;
				for (int i = blocks.Count - 1; i >= 0; i--)
				{
					var block = blocks[i];
					var blockLiveness = liveness[block];

					BitArray liveOut = new BitArray(values.Length);
					foreach (var successor in block.Targets)
					{
						var successorLiveness = liveness[successor];
						if (successorLiveness.In != null)
							liveOut.Or(successorLiveness.In);

						// Since the operands of phi functions are not processed,
						// set the source operands of the successor's phi functions in this block alive
						foreach (var phi in successor.Body)
						{
							if (phi.OpCode != IROpCodes.Phi)
								break;
							for (int j = 0; j < phi.Operands.Count; j++)
							{
								PhiOperand operand = (PhiOperand)phi.Operands[j];
								if (operand.SourceBlock == block)
									liveOut.Set(Array.IndexOf(values, operand.Value), true);
							}
						}
					}

					if (blockLiveness.Out == null || (!changed && !blockLiveness.Out.AreSame(liveOut)))
						changed = true;
					blockLiveness.Out = liveOut;

					BitArray liveIn = new BitArray(liveOut);
					liveIn.And(blockLiveness.KillNot).Or(blockLiveness.Gen);

					if (blockLiveness.In == null || (!changed && !blockLiveness.In.AreSame(liveIn)))
						changed = true;
					blockLiveness.In = liveIn;
				}

			} while (changed);
		}

		void BuildLiveIntervals()
		{
			// See Figure 11 of ftp://ftp.ssw.uni-linz.ac.at/pub/Papers/Moe02.PDF for detail of the cases

			foreach (var block in blocks)
			{
				var blockLiveness = liveness[block];
				uint first = blockLiveness.ActualBegin.Sequence;
				uint last = block.Body.Last.Value.Sequence;

				for (int i = 0; i < values.Length; i++)
				{
					var value = values[i];
					if (value.Definition == null || value.Usages.Count == 0)    // TODO: definition should not be null, add local inits
						continue;

					var defSeq = value.Definition.Sequence;
					if ((!blockLiveness.In[i] && instrParent[defSeq] != block))
						continue;

					// Ensure no phi functions are included in live intervals
					if (value.Definition.OpCode == IROpCodes.Phi)
						defSeq = first;

					Instruction end = null;
					if (!blockLiveness.Out[i])
					{
						// Find the farthest usage in the block
						foreach (var usage in value.Usages)
							if (instrParent[usage.Sequence] == block &&
								(end == null || usage.Sequence > end.Sequence))
							{
								end = usage;
							}
					}

					// Ensure no phi functions are included in live intervals
					if (end != null && end.OpCode == IROpCodes.Phi)
						end = blockLiveness.ActualBegin;

					liveIntervals[value].AddInterval(blockLiveness.In[i] ? first : defSeq, end == null ? last : end.Sequence);
				}
			}
		}

		public void Run()
		{
			var valueSet = new HashSet<Value>(CollectValues());
			values = new Value[valueSet.Count];
			valueSet.CopyTo(values);

			ComputeLocalLiveness();

			ComputeGlobalLiveness();

			BuildLiveIntervals();
		}
	}
}
