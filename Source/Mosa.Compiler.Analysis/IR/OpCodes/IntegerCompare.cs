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
	/// Intermediate representation of an integer comparison operation.
	/// </summary>
	public sealed class IntegerCompare : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IntegerCompare"/> class.
		/// </summary>
		public IntegerCompare()
			: base(2, 1)
		{
		}

		/// <summary>
		/// Abstract visitor method for intermediate representation visitors.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.IntegerCompare(ptr);
		}
	}
}