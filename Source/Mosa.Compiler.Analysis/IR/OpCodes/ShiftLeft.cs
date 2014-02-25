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
	/// Intermediate representation of the left shift operation.
	/// </summary>
	/// <remarks>
	/// The shift instruction is a three-address instruction, where the result receives
	/// the value of the first operand shifted by the number of bits specified by
	/// the second operand.
	/// <para />
	/// Both the first and second operand must be the same integral type. If the second operand
	/// is statically or dynamically equal to or larger than the number of bits in the first
	/// operand, the result is undefined.
	/// </remarks>
	public sealed class ShiftLeft : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShiftLeft"/> class.
		/// </summary>
		public ShiftLeft()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.ShiftLeft(ptr);
		}
	}
}