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
	internal class Pop : CILOpCode
	{
		public Pop()
			:base(CILCode.Pop)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			ctx.EvaluationStack.Pop();
		}
	}
}
