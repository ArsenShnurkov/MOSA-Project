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
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Compare : CILOpCode
	{
		public Compare(CILCode code)
			: base(code)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			var result = ctx.CreateVReg(ctx.TypeSystem.BuiltIn.Boolean);

			var a = ctx.EvaluationStack.Pop();
			var b = ctx.EvaluationStack.Pop();
			Debug.Assert(a.Type.GetStackTypeCode() == b.Type.GetStackTypeCode());

			ctx.IRPointer.Append()
				.SetOpCode(a.Type.GetComparisonOpCode())
				.SetOperand1(a.ToOperand())
				.SetOperand2(b.ToOperand())
				.SetResult(result)
				.SetExtra(base.Code.ToConditionCode());

			ctx.EvaluationStack.Push(result);
		}
	}
}
