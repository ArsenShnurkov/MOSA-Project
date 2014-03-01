/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.Compiler.Analysis
{
	public abstract class OpCode
	{
		protected OpCode(int operandCount, int resultCount)
		{
			OperandCount = operandCount;
			ResultCount = resultCount;
		}

		public int OperandCount { get; private set; }

		public int ResultCount { get; private set; }

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public virtual void Visit(IVisitor visitor, InstrPointer ptr)
		{
		}

		public override string ToString()
		{
			return GetType().Name;
		}
	}
}
