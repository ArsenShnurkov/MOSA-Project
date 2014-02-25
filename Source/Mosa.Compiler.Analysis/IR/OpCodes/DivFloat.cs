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
	/// Intermediate representation of the floating point division operation.
	/// </summary>
	/// <remarks>
	/// The instruction is a three-address instruction, where the result receives
	/// the value of the first operand divided by the second operand.
	/// <para />
	/// Both the first and second operand must be the same floating point type. If the second operand
	/// is statically or dynamically equal to or larger than the number of bits in the first
	/// operand, the result is undefined.
	/// </remarks>
	public sealed class DivFloat : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DivFloat"/> class.
		/// </summary>
		public DivFloat()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.DivFloat(ptr);
		}
	}
}