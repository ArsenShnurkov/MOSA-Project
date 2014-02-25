/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.IR;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Nop : CILOpCode
	{
		public Nop()
			:base(CILCode.Nop)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			ctx.IRPointer.Append().SetOpCode(IROpCodes.Nop);
		}
	}
}
