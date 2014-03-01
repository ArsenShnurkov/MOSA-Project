/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using Mosa.Compiler.Analysis.Blocks;

namespace Mosa.Compiler.Analysis
{
	public class InstrPointer
	{
		public InstrPointer()
		{
			current = null;
			offset = 0xffffffff;
		}
		public InstrPointer(BasicBlock block)
		{
			current = block.Body.First;
			offset = current.Value.OriginOffset;
		}

		private LinkedListNode<Instruction> current;
		private uint offset;

		public uint Offset { get { return offset; } set { offset = value; } }

		public Instruction Instruction { get { return current.Value; } }

		public InstrPointer SetBlock(BasicBlock block)
		{
			current = block.Body.First;
			offset = current.Value.OriginOffset;
			return this;
		}

		public bool HasNext()
		{
			return current.Next != null;
		}

		public InstrPointer Next()
		{
			current = current.Next;
			offset = current.Value.OriginOffset;
			return this;
		}

		public Instruction GetNext()
		{
			return current.Next == null ? null : current.Next.Value;
		}

		public bool HasPrevious()
		{
			return current.Previous != null;
		}

		public InstrPointer Previous()
		{
			current = current.Previous;
			offset = current.Value.OriginOffset;
			return this;
		}

		public Instruction GetPrevious()
		{
			return current.Next == null ? null : current.Previous.Value;
		}

		public InstrPointer First()
		{
			current = current.List.First;
			offset = current.Value.OriginOffset;
			return this;
		}

		public InstrPointer Last()
		{
			current = current.List.Last;
			offset = current.Value.OriginOffset;
			return this;
		}

		public InstrPointer InsertPrevious()
		{
			current = current.List.AddBefore(current, new Instruction()
			{
				OriginOffset = offset
			});
			return this;
		}

		public InstrPointer Append()
		{
			current = current.List.AddAfter(current, new Instruction()
			{
				OriginOffset = offset
			});
			return this;
		}

		public InstrPointer Clear()
		{
			current.Value.Clear();
			current.Value.OriginOffset = offset;
			return this;
		}

		public InstrPointer Remove()
		{
			var newPos = current.Next ?? current.Previous;
			current.List.Remove(current);
			current = newPos;
			return this;
		}

		public override string ToString()
		{
			if (current == null)
				return "<<NULL>>";
			return Instruction.ToString();
		}

		public InstrPointer SetOpCode(OpCode opCode)
		{
			Instruction.OpCode = opCode;
			return this;
		}

		public InstrPointer SetOperand1(Operand operand)
		{
			Instruction.Operand1 = operand;
			return this;
		}

		public InstrPointer SetOperand2(Operand operand)
		{
			Instruction.Operand2 = operand;
			return this;
		}

		public InstrPointer SetOperands(Operand[] operands)
		{
			Instruction.SetOperands(operands);
			return this;
		}

		public InstrPointer SetResult(Value result)
		{
			Instruction.Result = result;
			return this;
		}

		public InstrPointer SetExtra(object value)
		{
			Instruction.Extra = value;
			return this;
		}
	}
}
