/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

namespace Mosa.Compiler.Analysis.IR.OpCodes
{
	/// <summary>
	/// Intermediate representation of an arbitrary sign extended move operation.
	/// </summary>
	/// <remarks>
	/// This instruction moves the source operand to the result and converts to the request size maintaining its sign.
	/// </remarks>
	public sealed class MoveSignExtended : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveSignExtended"/> class.
		/// </summary>
		public MoveSignExtended()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.MoveSignExtended(ptr);
		}
	}
}