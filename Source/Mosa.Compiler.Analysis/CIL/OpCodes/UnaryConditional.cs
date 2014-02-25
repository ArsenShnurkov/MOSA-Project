/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class UnaryConditional : CILOpCode
	{
		public UnaryConditional(CILCode code)
			: base(code)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			int target = (int)ctx.Instruction.Operand;

			var cond = ctx.EvaluationStack.Pop();
			LabelOperand jump;
			LabelOperand fallThrough;
			if (base.Code == CILCode.Brtrue || base.Code == CILCode.Brtrue_s)
			{
				jump = LabelOperand.Create(ctx.BlockHeaders[target]);
				fallThrough = LabelOperand.Create(ctx.BlockHeaders[ctx.Instruction.Next.Value]);
			}
			else
			{
				jump = LabelOperand.Create(ctx.BlockHeaders[ctx.Instruction.Next.Value]);
				fallThrough = LabelOperand.Create(ctx.BlockHeaders[target]);
			}

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.CondBranch)
				.SetOperand1(cond.ToOperand())
				.SetOperand2(jump);

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Branch)
				.SetOperand1(fallThrough);
		}

		public override FlowControl FlowControl { get { return CIL.FlowControl.ConditionalBranch; } }
	}
}
