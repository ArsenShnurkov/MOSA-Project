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
	internal class Ldarg : CILOpCode
	{
		public Ldarg(CILCode code)
			: base(code)
		{
			switch (code)
			{
				case CILCode.Ldarg_0: index = 0; break;
				case CILCode.Ldarg_1: index = 1; break;
				case CILCode.Ldarg_2: index = 2; break;
				case CILCode.Ldarg_3: index = 3; break;
			}
		}

		int index = -1;

		public override void Parse(TransformContext ctx)
		{
			int index = this.index;
			if (index == -1)
			{
				index = (int)ctx.Instruction.Operand;
			}
			var arg = ctx.GetParameter(index);
			var result = ctx.CreateVReg(arg.Type);

			ctx.IRPointer.Append()
				.SetOpCode(arg.Type.GetLoadOpCode())
				.SetResult(result)
				.SetOperand1(arg.ToOperand());

			ctx.EvaluationStack.Push(result);
		}
	}
}
