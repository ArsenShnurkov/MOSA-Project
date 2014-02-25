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
	/// An abstract intermediate representation of the method prologue.
	/// </summary>
	/// <remarks>
	/// This opcode is usually replaced by the architecture and expanded appropriately
	/// for the calling convention of the method.
	/// </remarks>
	public sealed class Prologue : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Prologue"/> class.
		/// </summary>
		public Prologue()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Prologue(ptr);
		}
	}
}