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
	/// Intermediate representation of the method epilogue.
	/// </summary>
	/// <remarks>
	/// This opcode is usually replaced by the architecture and expanded appropriately
	/// for the calling convention of the method.
	/// </remarks>
	public sealed class Epilogue : IROpCode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Epilogue"/> class.
		/// </summary>
		public Epilogue()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this opcode object.
		/// </summary>
		/// <param name="visitor">The visitor.</param>
		/// <param name="ptr">The instruction pointer.</param>
		public override void Visit(IIRVisitor visitor, InstrPointer ptr)
		{
			visitor.Epilogue(ptr);
		}
	}
}