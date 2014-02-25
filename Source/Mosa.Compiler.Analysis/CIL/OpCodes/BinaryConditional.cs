/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Diagnostics;
using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class BinaryConditional : CILOpCode
	{
		public BinaryConditional(CILCode code)
			: base(code)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			int target = (int)ctx.Instruction.Operand;

			var cmp = ctx.CreateVReg(ctx.TypeSystem.BuiltIn.Boolean);
			var a = ctx.EvaluationStack.Pop();
			var b = ctx.EvaluationStack.Pop();
			Debug.Assert(a.Type.GetStackTypeCode() == b.Type.GetStackTypeCode());

			ctx.IRPointer.Append()
				.SetOpCode(a.Type.GetComparisonOpCode())
				.SetOperand1(a.ToOperand())
				.SetOperand2(b.ToOperand())
				.SetResult(cmp)
				.SetExtra(base.Code.ToConditionCode());

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.CondBranch)
				.SetOperand1(cmp.ToOperand())
				.SetOperand2(LabelOperand.Create(ctx.BlockHeaders[target]));

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Branch)
				.SetOperand1(LabelOperand.Create(ctx.BlockHeaders[ctx.Instruction.Next.Value]));
		}

		public override FlowControl FlowControl { get { return CIL.FlowControl.ConditionalBranch; } }
	}
}
