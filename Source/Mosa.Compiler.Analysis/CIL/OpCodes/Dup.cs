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
	internal class Dup : CILOpCode
	{
		public Dup()
			:base(CILCode.Dup)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			var value = ctx.EvaluationStack.Pop();
			var dup1 = ctx.CreateVReg(value.Type);
			var dup2 = ctx.CreateVReg(value.Type);
			ctx.EvaluationStack.Push(dup1);
			ctx.EvaluationStack.Push(dup2);

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Move)
				.SetResult(dup1)
				.SetOperand1(value.ToOperand());

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Move)
				.SetResult(dup2)
				.SetOperand1(value.ToOperand());
		}
	}
}
