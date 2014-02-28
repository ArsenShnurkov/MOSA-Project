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
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Shift : CILOpCode
	{
		public Shift(CILCode code)
			: base(code)
		{
		}

		// Assume code is verificable
		public override void Parse(TransformContext ctx)
		{
			var shiftAmount = ctx.EvaluationStack.Pop();
			var val = ctx.EvaluationStack.Pop();
			var result = ctx.CreateVReg(val.Type);

			switch ((CILCode)ctx.Instruction.OpCode)
			{
				case CILCode.Shl:
					ctx.IRPointer.Append()
						.SetOpCode(IROpCodes.ShiftLeft)
						.SetOperand1(val.ToOperand())
						.SetOperand2(shiftAmount.ToOperand())
						.SetResult(result);
					break;

				case CILCode.Shr:
					ctx.IRPointer.Append()
						.SetOpCode(IROpCodes.ArithmeticShiftRight)
						.SetOperand1(val.ToOperand())
						.SetOperand2(shiftAmount.ToOperand())
						.SetResult(result);
					break;

				case CILCode.Shr_un:
					ctx.IRPointer.Append()
						.SetOpCode(IROpCodes.ShiftRight)
						.SetOperand1(val.ToOperand())
						.SetOperand2(shiftAmount.ToOperand())
						.SetResult(result);
					break;

				default:
					throw new InvalidCompilerException();
			}

			ctx.EvaluationStack.Push(result);
		}
	}
}
