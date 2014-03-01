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
	/// Intermediate representation of a breakpoint embedded in code.
	/// </summary>
	public sealed class Break : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Break"/> class.
		/// </summary>
		public Break()
			: base(0, 0)
		{
		}

		/// <summary>
		/// Visits the specified visitor.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Break(ptr);
		}
	}
}