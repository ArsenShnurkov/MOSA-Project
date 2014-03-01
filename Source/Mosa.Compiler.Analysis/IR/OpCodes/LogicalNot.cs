/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Simon Wollwage (rootnode) <kintaro@think-in-co.de>
 */

namespace Mosa.Compiler.Analysis.IR.OpCodes
{
	/// <summary>
	/// Intermediate representation of the NOT operation.
	/// </summary>
	public sealed class LogicalNot : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LogicalNot"/> class.
		/// </summary>
		public LogicalNot()
			: base(1, 1)
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.LogicalNot(ptr);
		}
	}
}