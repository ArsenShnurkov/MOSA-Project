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
	/// Intermediate representation of an arbitrary zero extended move operation.
	/// </summary>
	/// <remarks>
	/// This instruction moves the source operand to the result and converts to the request size disregarding its sign.
	/// </remarks>
	public sealed class MoveZeroExtended : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MoveZeroExtended"/> class.
		/// </summary>
		public MoveZeroExtended()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.MoveZeroExtended(ptr);
		}
	}
}