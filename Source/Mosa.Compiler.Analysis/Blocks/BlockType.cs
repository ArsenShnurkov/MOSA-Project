/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

namespace Mosa.Compiler.Analysis.Blocks
{
	public enum BlockType
	{
		Method,
		TryException,
		TryFinally,
		TryFilter,
		TryFault,
		Handler,
		Filter,

		Abnormal,
		Return
	}
}
