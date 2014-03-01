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
	/// Intermediate representation of an unconditional branch operation.
	/// </summary>
	public sealed class Branch : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Branch"/> class.
		/// </summary>
		public Branch()
			: base(1, 0)
		{
		}

		/// <summary>
		/// Visits the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Branch(ptr);
		}
	}
}