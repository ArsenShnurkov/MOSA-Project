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
	internal class Neg : CILOpCode
	{
		public Neg()
			: base(CILCode.Neg)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			var val = ctx.EvaluationStack.Pop();
			var result = ctx.Body.CreateVReg(val.Type);

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Negate)
				.SetOperand1(val.ToOperand())
				.SetResult(result);
		}
	}
}
