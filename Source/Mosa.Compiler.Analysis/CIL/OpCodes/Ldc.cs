/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.Common;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Ldc : CILOpCode
	{
		public Ldc(CILCode code)
			: base(code)
		{
			switch (code)
			{
				case CILCode.Ldc_i4:
				case CILCode.Ldc_i4_s:
					type = MosaTypeCode.I4;
					break;

				case CILCode.Ldc_i8:
					type = MosaTypeCode.I8;
					break;

				case CILCode.Ldc_r4:
					type = MosaTypeCode.R4;
					break;

				case CILCode.Ldc_r8:
					type = MosaTypeCode.R8;
					break;

				case CILCode.Ldnull:
					type = MosaTypeCode.Object;
					break;

				case CILCode.Ldstr:
					type = MosaTypeCode.String;
					break;

				case CILCode.Ldc_i4_0: value = (int)0; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_1: value = (int)1; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_2: value = (int)2; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_3: value = (int)3; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_4: value = (int)4; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_5: value = (int)5; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_6: value = (int)6; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_7: value = (int)7; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_8: value = (int)8; goto case CILCode.Ldc_i4;
				case CILCode.Ldc_i4_m1: value = (int)-1; goto case CILCode.Ldc_i4;
				default: throw new NotImplementCompilerException();
			}
		}

		object value;
		MosaTypeCode type;

		public override void Parse(TransformContext ctx)
		{
			object value = this.value;
			if (value == null)
			{
				switch ((CILCode)ctx.Instruction.OpCode)
				{
					case CILCode.Ldc_i4:
						value = (int)ctx.Instruction.Operand;
						break;

					case CILCode.Ldc_i4_s:
						value = (int)(sbyte)ctx.Instruction.Operand;
						break;

					case CILCode.Ldc_i8:
						value = (long)ctx.Instruction.Operand;
						break;

					case CILCode.Ldc_r4:
						value = (float)ctx.Instruction.Operand;
						break;

					case CILCode.Ldc_r8:
						value = (double)ctx.Instruction.Operand;
						break;

					case CILCode.Ldstr:
						value = (string)ctx.TypeSystem.LookupUserString(ctx.Body.Method.Module, (uint)ctx.Instruction.Operand);
						break;
				}
			}
			ctx.EvaluationStack.Push(ConstantValue.Create(ctx.TypeSystem, value, type));
		}
	}
}
