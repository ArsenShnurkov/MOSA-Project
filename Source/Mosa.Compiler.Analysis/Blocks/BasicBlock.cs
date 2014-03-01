/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using System.Text;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Blocks
{
	public class BasicBlock : BlockBase
	{
		public int Sequence { get; private set; }

		public uint? BeginOffset
		{
			get
			{
				if (Body.Count == 0)
					return null;
				return Body.First.Value.OriginOffset;
			}
		}

		internal IList<MosaInstruction> CILInstructions { get; private set; }

		internal MosaInstruction TerminatorInstruction { get; set; }

		internal int GetCILOffset()
		{
			if (CILInstructions.Count == 0)
				return TerminatorInstruction.Offset;
			else
				return CILInstructions[0].Offset;
		}

		public LinkedList<Instruction> Body { get; private set; }

		public List<BasicBlock> Sources { get; private set; }

		public List<BasicBlock> Targets { get; private set; }

		public bool IsTechnicalBlock { get; set; }

		public BasicBlock(int id)
		{
			this.Sequence = id;
			Body = new LinkedList<Instruction>();
			CILInstructions = new List<MosaInstruction>();
			Sources = new List<BasicBlock>(2);
			Targets = new List<BasicBlock>(2);
		}

		public static void Link(BasicBlock source, BasicBlock target)
		{
			source.Targets.Add(target);
			target.Sources.Add(source);
		}

		public static void Unlink(BasicBlock source, BasicBlock target)
		{
			source.Targets.Remove(target);
			target.Sources.Remove(source);
		}

		public IEnumerable<BasicBlock> Postorder()
		{
			List<BasicBlock> visited = new List<BasicBlock>();
			Stack<BasicBlock> stack = new Stack<BasicBlock>();

			stack.Push(this);

			while (stack.Count > 0)
			{
				BasicBlock current = stack.Peek();
				if (!visited.Contains(current))
				{
					visited.Add(current);
					foreach (BasicBlock target in current.Targets)
					{
						if (!visited.Contains(target))
							stack.Push(target);
					}
				}
				else
					yield return stack.Pop();
			}
		}

		public IList<BasicBlock> ReversePostOrder()
		{
			var result = new List<BasicBlock>(Postorder());
			result.Reverse();
			return result;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Block_" + Sequence + ":");
			foreach (var instr in Body)
				result.AppendLine(instr.ToString());
			return result.ToString();
		}
	}
}
