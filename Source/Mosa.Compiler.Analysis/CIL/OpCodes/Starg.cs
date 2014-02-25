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
	internal class Starg : CILOpCode
	{
		public Starg(CILCode code)
			: base(code)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			int index = (int)ctx.Instruction.Operand;

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Move)
				.SetOperand1(ctx.EvaluationStack.Pop().ToOperand())
				.SetResult(ctx.GetParameter(index));
		}
	}
}
