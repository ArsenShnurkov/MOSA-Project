/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.Compiler.Analysis.CIL
{
	/// <summary>
	/// Specifies flow-control properties of an instruction.
	/// </summary>
	internal enum FlowControl
	{
		/// <summary>
		/// The instruction always continues execution on the next instruction.
		/// </summary>
		Next = 0x00,

		/// <summary>
		/// The instruction invokes another method.
		/// </summary>
		Call = 0x01,

		/// <summary>
		/// The instruction is an unconditional branch.
		/// </summary>
		UnconditionalBranch = 0x02,

		/// <summary>
		/// The instruction is a conditional branch, which never falls through.
		/// </summary>
		ConditionalBranch = 0x04,

		/// <summary>
		/// The instruction is a conditional branch, which may fall-through.
		/// </summary>
		Switch = 0x08,

		/// <summary>
		/// The instruction returns from the method
		/// </summary>
		Return = 0x10,

		/// <summary>
		/// The instruction throws an exception.
		/// </summary>
		Throw = 0x20,

		/// <summary>
		/// The instruction leaves a protected block
		/// </summary>
		Leave = 0x40,

		/// <summary>
		/// The instruction returns from the handler
		/// </summary>
		Handler = 0x80,
	}
}