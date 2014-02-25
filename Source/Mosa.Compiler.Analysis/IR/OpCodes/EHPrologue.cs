/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */
namespace Mosa.Compiler.Analysis.IR.OpCodes
{
	/// <summary>
	/// Intermediate representation of the exception handler prologue.
	/// </summary>
	/// <remarks>
	/// This opcode is usually replaced by the architecture and expanded appropriately
	/// for the calling convention of the method.
	/// </remarks>
	public sealed class EHPrologue : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EHPrologue"/> class.
		/// </summary>
		public EHPrologue()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.EHPrologue(ptr);
		}
	}
}