/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using System.Diagnostics;
using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.CIL;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis.IR
{
	public class IRTransformer
	{
		/* 
		 * There are two kinds of phi function nodes:
		 * 1. Local variables => placed using DF at the beginning, arguments of phi function populated while parsing CIL.
		 * 2. Stack remains => placed by the source block to converging block, arguments of phi function populated by source blocks
		 * 
		 * References:
		 * (1): http://ssabook.gforge.inria.fr/latest/book.pdf
		 * (2): http://community.sharpdevelop.net/blogs/dsrbecky/archive/2011/02/19/ilspy-ensuring-correctness.aspx
		 * 
		 */

		// Assume code is verificable
		public static void Transform(MethodBody body)
		{
			var blocks = body.EntryBlock.ReversePostOrder();
			Debug.Assert(blocks[0] == body.EntryBlock);

			TransformContext ctx = new TransformContext(body, blocks);

			// Setup initial block state
			ctx.BlockInfos[body.EntryBlock.Sequence] = new BlockInfo(body.EntryBlock)
			{
				EvalutionStack = new EvaluationStack(body.Method.MaxStack),
				SSAVersion = new int[body.Locals.Count]
			};

			// Initialize block bodys
			foreach (var block in blocks)
			{
				block.Body.AddFirst(new Instruction()
				{
					OriginOffset = (uint)block.GetCILOffset(),
					OpCode = IROpCodes.Nop
				});
			}

			// Place phi functions
			InitializePhiFunctions(body, blocks, ctx);

			Queue<BasicBlock> workList = new Queue<BasicBlock>();
			workList.Enqueue(body.EntryBlock);
			while (workList.Count > 0)
			{
				var block = workList.Dequeue();
				Debug.Assert(ctx.BlockInfos.ContainsKey(block.Sequence));

				var info = ctx.BlockInfos[block.Sequence];
				ctx.SetBlock(block);
				ctx.EvaluationStack = info.EvalutionStack;

				foreach (var instr in block.CILInstructions)
				{
					ctx.IRPointer.Offset = (uint)instr.Offset;
					ctx.Instruction = instr;
					CILOpCodes.OpCodes[(int)instr.OpCode].Parse(ctx);
				}

				var beforeBranch = ctx.IRPointer;

				if (block.TerminatorInstruction != null)
				{
					ctx.IRPointer.Offset = (uint)block.TerminatorInstruction.Offset;
					ctx.Instruction = block.TerminatorInstruction;
					CILOpCodes.OpCodes[(int)block.TerminatorInstruction.OpCode].Parse(ctx);
				}
				else if (block.Targets.Count == 1)    // i.e. fall through to next block
				{
					ctx.IRPointer.Append()
						.SetOpCode(IROpCodes.Branch)
						.SetOperand1(LabelOperand.Create(block.Targets[0]));
				}
				else
					Debug.Assert(block.Targets.Count == 0);

				// Propagate SSA versions & stack information

				PropagateSSAVers(body, info, beforeBranch, ctx);

				foreach (var next in PropagateStack(body, info, beforeBranch, ctx))
					workList.Enqueue(next);

				info.Status = BlockInfo.StatusProcessed;
			}
			// Set phi function operands
			FinalizePhis(ctx);
			CleanupBody(body, blocks);
		}

		static void InitializePhiFunctions(MethodBody body, IList<BasicBlock> blocks, TransformContext ctx)
		{
			// Algorithm 3.1 at (1)
			List<BasicBlock>[] defs = new List<BasicBlock>[body.Locals.Count];
			for (int i = 0; i < body.Locals.Count; i++)
				defs[i] = new List<BasicBlock>();

			foreach (var block in blocks)
				foreach (var instr in block.CILInstructions)
				{
					int? localIndex = instr.GetStlocIndex();
					if (localIndex != null && !defs[localIndex.Value].Contains(block))
						defs[localIndex.Value].Add(block);
				}

			List<BasicBlock> insertedPhi = new List<BasicBlock>();
			Queue<BasicBlock> workList = new Queue<BasicBlock>();
			for (int i = 0; i < body.Locals.Count; i++)
			{
				insertedPhi.Clear();
				List<BasicBlock> varDefs = defs[i];
				foreach (var block in varDefs)
					workList.Enqueue(block);

				InstrPointer ptr = new InstrPointer();
				while (workList.Count > 0)
				{
					BasicBlock x = workList.Dequeue();
					foreach (var y in ctx.Dominance.GetDominanceFrontier(x))
					{
						if (!insertedPhi.Contains(y))
						{
							BlockInfo blockInfo;
							if (!ctx.BlockInfos.TryGetValue(y.Sequence, out blockInfo))
							{
								ctx.BlockInfos[y.Sequence] = blockInfo = new BlockInfo(y);
								blockInfo.LocalPhis = new Tuple<Instruction, SSAValue[]>[body.Locals.Count];
							}

							// Insert phi function
							ptr.SetBlock(y).InsertPrevious();
							var phiValue = ctx.GetLocalNewVersion(i);

							ptr.SetOpCode(IROpCodes.Phi)
								.SetResult(phiValue);
							blockInfo.LocalPhis[i] = Tuple.Create(ptr.Instruction, new SSAValue[y.Sources.Count]);

							insertedPhi.Add(y);
							if (!varDefs.Contains(y))
								workList.Enqueue(y);
						}
					}
				}
			}
		}

		static void PropagateSSAVers(MethodBody body, BlockInfo info, InstrPointer insertionPt, TransformContext ctx)
		{
			foreach (var target in info.Block.Targets)
			{
				int srcIndex = target.Sources.IndexOf(info.Block);

				BlockInfo targetInfo;
				if (!ctx.BlockInfos.TryGetValue(target.Sequence, out targetInfo))
				{
					// Not yet created => No phi => Initialize SSA versions

					ctx.BlockInfos[target.Sequence] = targetInfo = new BlockInfo(target);

					targetInfo.SSAVersion = (int[])info.SSAVersion.Clone();
				}
				else
				{
					// Already created => Propagate SSA versions
					if (targetInfo.SSAVersion == null)
						targetInfo.SSAVersion = (int[])info.SSAVersion.Clone();
					for (int i = 0; i < body.Locals.Count; i++)
					{
						if (targetInfo.LocalPhis != null && targetInfo.LocalPhis[i] != null)
						{
							// Phi function exists for this local variable => set corresponding phi argument
							var phiValue = (SSAValue)targetInfo.LocalPhis[i].Item1.Result;
							targetInfo.SSAVersion[i] = phiValue.Version;
							targetInfo.LocalPhis[i].Item2[srcIndex] = ctx.GetLocalVersion(i, info.SSAVersion[i]);
						}
						else
							Debug.Assert(targetInfo.SSAVersion[i] == info.SSAVersion[i]);
					}
				}
			}
		}

		static IEnumerable<BasicBlock> PropagateStack(MethodBody body, BlockInfo info, InstrPointer insertionPt, TransformContext ctx)
		{
			foreach (var target in info.Block.Targets)
			{
				BlockInfo targetInfo = ctx.BlockInfos[target.Sequence];
				if (targetInfo.Status == BlockInfo.StatusAwait)
				{
					// Not yet processed => Initialize stack

					yield return target;

					if (targetInfo == null)
					{
						ctx.BlockInfos[target.Sequence] = targetInfo = new BlockInfo(target);
					}
					targetInfo.Status = BlockInfo.StatusQueued;

					if (target.Sources.Count > 0 && info.EvalutionStack.Depth > 0)
					{
						// Have a stack merge

						// Create temporary virtual registers for the merge
						targetInfo.StackValues = new VRegValue[info.EvalutionStack.Depth];
						targetInfo.EvalutionStack = new EvaluationStack(body.Method.MaxStack);
						for (int i = 0; i < targetInfo.StackValues.Length; i++)
						{
							targetInfo.StackValues[i] = body.CreateVReg(info.EvalutionStack.Stack[i].Type);
						}

						ProcessStackMerge(info, insertionPt, targetInfo, true);
					}
					else if (info.EvalutionStack.Depth > 0)
					{
						// Single forward flow => Only copy the stack
						targetInfo.EvalutionStack = new EvaluationStack(info.EvalutionStack);
					}
					else
					{
						// No stack remains => New stack
						targetInfo.EvalutionStack = new EvaluationStack(body.Method.MaxStack);
					}
				}
				else
				{
					// Pending / Already processed => Copy stack
					if (target.Sources.Count > 0 && targetInfo.StackValues != null)
					{
						Debug.Assert(targetInfo.StackValues.Length == info.EvalutionStack.Depth);
						// Target has phi functions => Merge current stack to target
						ProcessStackMerge(info, insertionPt, targetInfo, false);
					}
				}
			}
		}

		static void ProcessStackMerge(BlockInfo source, InstrPointer insertionPt, BlockInfo target, bool insertPhi)
		{
			int srcIndex = target.Block.Sources.IndexOf(source.Block);
			SSAValue[] vars = new SSAValue[target.StackValues.Length];

			// Insert store instructions in current block
			// SSA version ID is the source block index of target block
			InstrPointer ptr = insertionPt;
			for (int i = 0; i < target.StackValues.Length; i++)
			{
				vars[i] = new SSAValue(target.StackValues[i], srcIndex);
				ptr.Append()
					.SetOpCode(IROpCodes.Move)
					.SetOperand1(source.EvalutionStack.Stack[i].ToOperand())
					.SetResult(vars[i]);
			}

			if (insertPhi)
			{
				// Insert phi instructions in target block
				ptr.SetBlock(target.Block).First();

				target.StackPhis = new Tuple<Instruction, SSAValue[]>[target.StackValues.Length];
				for (int i = 0; i < target.StackValues.Length; i++)
				{
					SSAValue[] versions = new SSAValue[target.Block.Sources.Count];
					versions[srcIndex] = vars[i];
					target.StackPhis[i] = Tuple.Create(
						ptr.Append()
						.SetOpCode(IROpCodes.Phi)
						.SetResult(target.StackValues[i])
						.Instruction,
						versions);

					// Final value's version ID is number of source blocks
					SSAValue phiValue = new SSAValue(target.StackValues[i], target.Block.Sources.Count);

					ptr.SetOpCode(IROpCodes.Move)
						.SetResult(phiValue);

					target.EvalutionStack.Push(phiValue);
				}
			}
			else
			{
				// Add current versions to target phi function
				for (int i = 0; i < target.StackValues.Length; i++)
				{
					target.StackPhis[i].Item2[srcIndex] = vars[i];
				}
			}
		}

		static void FinalizePhis(TransformContext ctx)
		{
			foreach (var info in ctx.BlockInfos.Values)
			{
				if (info.StackPhis != null)
				{
					foreach (var phi in info.StackPhis)
					{
						Operand[] operands = new Operand[phi.Item2.Length];
						for (int i = 0; i < operands.Length; i++)
							operands[i] = phi.Item2[i].ToOperand();
						phi.Item1.SetOperands(operands);
					}
				}

				if (info.LocalPhis != null)
				{
					foreach (var phi in info.LocalPhis)
						if (phi != null)
						{
							Operand[] operands = new Operand[phi.Item2.Length];
							for (int i = 0; i < operands.Length; i++)
								operands[i] = phi.Item2[i].ToOperand();
							phi.Item1.SetOperands(operands);
						}
				}
			}
		}

		static void CleanupBody(MethodBody body, IList<BasicBlock> blocks)
		{
			InstrPointer ptr = new InstrPointer();

			ptr.SetBlock(body.EntryBlock);
			ptr.InsertPrevious().SetOpCode(IROpCodes.Prologue);
			ptr.SetBlock(body.ReturnBlock).Last();
			ptr.Append().SetOpCode(IROpCodes.Epilogue);


			foreach (var block in blocks)
			{
				ptr.SetBlock(block);
				while (ptr.HasNext())
				{
					if (ptr.Instruction.OpCode == IROpCodes.Nop && ptr.GetNext().OriginOffset == ptr.Instruction.OriginOffset)
						ptr.Remove();
					else
						ptr.Next();
				}
			}
		}
	}
}
