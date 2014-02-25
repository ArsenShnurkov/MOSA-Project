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
	public struct InstrPointer
	{
		public InstrPointer(BasicBlock block)
		{
			this.block = block;
			current = block.Body.First;
			offset = current.Value.CILOffset;
		}

		private BasicBlock block;
		private LinkedListNode<Instruction> current;
		private uint offset;

		public uint CILOffset { get { return offset; } set { offset = value; } }

		public Instruction Current { get { return current.Value; } }

		public BasicBlock Block { get { return block; } }

		public InstrPointer SetBlock(BasicBlock block)
		{
			this.block = block;
			current = block.Body.First;
			offset = current.Value.CILOffset;
			return this;
		}

		public bool HasNext()
		{
			return current.Next != null;
		}

		public InstrPointer Next()
		{
			current = current.Next;
			offset = current.Value.CILOffset;
			return this;
		}

		public bool HasPrevious()
		{
			return current.Previous != null;
		}

		public InstrPointer Previous()
		{
			current = current.Previous;
			offset = current.Value.CILOffset;
			return this;
		}

		public InstrPointer First()
		{
			current = block.Body.First;
			offset = current.Value.CILOffset;
			return this;
		}

		public InstrPointer Last()
		{
			current = block.Body.Last;
			offset = current.Value.CILOffset;
			return this;
		}

		public InstrPointer InsertPrevious()
		{
			current = block.Body.AddBefore(current, new Instruction()
			{
				CILOffset = offset,
				Parent = block
			});
			return this;
		}

		public InstrPointer Append()
		{
			current = block.Body.AddAfter(current, new Instruction()
			{
				CILOffset = offset,
				Parent = block
			});
			return this;
		}

		public InstrPointer Clear()
		{
			current.Value.Clear();
			current.Value.CILOffset = offset;
			return this;
		}

		public InstrPointer SetOpCode(OpCode opCode)
		{
			Current.OpCode = opCode;
			return this;
		}

		public InstrPointer SetOperand1(Operand operand)
		{
			Current.Operand1 = operand;
			return this;
		}

		public InstrPointer SetOperand2(Operand operand)
		{
			Current.Operand2 = operand;
			return this;
		}

		public InstrPointer SetResult(Value result)
		{
			Current.Result = result;
			return this;
		}

		public InstrPointer SetExtra(object value)
		{
			Current.Extra = value;
			return this;
		}
	}
}
