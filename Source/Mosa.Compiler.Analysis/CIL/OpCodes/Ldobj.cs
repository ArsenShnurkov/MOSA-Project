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
	internal class Ldobj : CILOpCode
	{
		private readonly MosaTypeCode? objType;

		public Ldobj(CILCode code)
			: base(code)
		{
			switch (code)
			{
				case CILCode.Ldind_i1: objType = MosaTypeCode.I1; break;
				case CILCode.Ldind_i2: objType = MosaTypeCode.I2; break;
				case CILCode.Ldind_i4: objType = MosaTypeCode.I4; break;
				case CILCode.Ldind_i8: objType = MosaTypeCode.I8; break;
				case CILCode.Ldind_u1: objType = MosaTypeCode.U1; break;
				case CILCode.Ldind_u2: objType = MosaTypeCode.U2; break;
				case CILCode.Ldind_u4: objType = MosaTypeCode.U4; break;
				case CILCode.Ldind_i: objType = MosaTypeCode.I; break;
				case CILCode.Ldind_r4: objType = MosaTypeCode.R4; break;
				case CILCode.Ldind_r8: objType = MosaTypeCode.R8; break;
				case CILCode.Ldind_ref: objType = MosaTypeCode.Object; break;
				case CILCode.Ldobj: objType = null; break;
				default: throw new InvalidCompilerException();
			}
		}

		public override void Parse(TransformContext ctx)
		{
			var adr = ctx.EvaluationStack.Pop();

			MosaType type;
			if (objType != null)
			{
				type = ctx.TypeSystem.GetTypeFromTypeCode(objType.Value);
				if (objType == MosaTypeCode.I8 && adr.Type.ElementType != null && adr.Type.ElementType.TypeCode == MosaTypeCode.U8)
					type = ctx.TypeSystem.BuiltIn.U8;
			}
			else
				type = (MosaType)ctx.Instruction.Operand;

			var result = ctx.CreateVReg(type);
			ctx.IRPointer.Append()
				.SetOpCode(type.GetLoadOpCode())
				.SetOperand1(adr.ToOperand())
				.SetResult(result);

			ctx.EvaluationStack.Push(result);
		}
	}
}