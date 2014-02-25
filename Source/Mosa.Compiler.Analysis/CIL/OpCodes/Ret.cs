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
	internal class Ret : CILOpCode
	{
		public Ret()
			: base(CILCode.Ret)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			if (!ctx.Body.Method.Signature.ReturnType.IsVoid)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.Return)
					.SetOperand1(ctx.EvaluationStack.Pop().ToOperand());
			}
			else
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.Return);
			}
		}

		public override FlowControl FlowControl { get { return CIL.FlowControl.Return; } }
	}
}
