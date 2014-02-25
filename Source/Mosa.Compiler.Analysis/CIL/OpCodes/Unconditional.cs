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
	internal class Unconditional : CILOpCode
	{
		public Unconditional(CILCode code)
			: base(code)
		{
		}

		public override void Parse(TransformContext ctx)
		{
			int target = (int)ctx.Instruction.Operand;

			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Branch)
				.SetOperand1(LabelOperand.Create(ctx.BlockHeaders[target]));
		}

		public override FlowControl FlowControl { get { return CIL.FlowControl.UnconditionalBranch; } }
	}
}
