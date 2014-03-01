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
	/// Intermediate representation of the load memory value operation.
	/// </summary>
	/// <remarks>
	/// The load instruction is used to load a value from a memory pointer. The types must be compatible.
	/// </remarks>
	public sealed class Load : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Load"/> class.
		/// </summary>
		public Load()
			: base(1, 1)
		{
		}

		/// <summary>
		/// Visitor method for intermediate representation visitors.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Load(ptr);
		}
	}
}