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
	internal class Not : CILOpCode
	{
		public Not()
			: base(CILCode.Not)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			var val = ctx.EvaluationStack.Pop();
			var result = ctx.Body.CreateVReg(val.Type);

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.LogicalNot)
				.SetOperand1(val.ToOperand())
				.SetResult(result);
		}
	}
}
