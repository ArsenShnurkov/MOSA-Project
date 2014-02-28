/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.MosaTypeSystem;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Stobj : CILOpCode
	{
		private readonly MosaTypeCode? objType;

		public Stobj(CILCode code)
			: base(code)
		{
			switch (code)
			{
				case CILCode.Stind_i1: objType = MosaTypeCode.I1; break;
				case CILCode.Stind_i2: objType = MosaTypeCode.I2; break;
				case CILCode.Stind_i4: objType = MosaTypeCode.I4; break;
				case CILCode.Stind_i8: objType = MosaTypeCode.I8; break;
				case CILCode.Stind_i: objType = MosaTypeCode.I; break;
				case CILCode.Stind_r4: objType = MosaTypeCode.R4; break;
				case CILCode.Stind_r8: objType = MosaTypeCode.R8; break;
				case CILCode.Stind_ref: objType = MosaTypeCode.Object; break;
				case CILCode.Stobj: objType = null; break;
				default: throw new InvalidCompilerException();
			}
		}

		public override void Parse(TransformContext ctx)
		{
			var val = ctx.EvaluationStack.Pop();
			var adr = ctx.EvaluationStack.Pop();

			MosaType type;
			if (objType != null)
				type = ctx.TypeSystem.GetTypeFromTypeCode(objType.Value);
			else
				type = (MosaType)ctx.Instruction.Operand;

			var result = ctx.CreateVReg(type);
			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Store)
				.SetOperand1(adr.ToOperand())
				.SetOperand2(val.ToOperand());
		}
	}
}