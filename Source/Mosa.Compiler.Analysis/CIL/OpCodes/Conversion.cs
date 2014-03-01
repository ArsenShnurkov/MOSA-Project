/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Common;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class Conversion : CILOpCode
	{
		public Conversion(CILCode code)
			: base(code)
		{
			switch (code)
			{
				case CILCode.Conv_u: type = MosaTypeCode.U; break;
				case CILCode.Conv_i: type = MosaTypeCode.I; break;
				case CILCode.Conv_i1: type = MosaTypeCode.I1; break;
				case CILCode.Conv_i2: type = MosaTypeCode.I2; break;
				case CILCode.Conv_i4: type = MosaTypeCode.I4; break;
				case CILCode.Conv_i8: type = MosaTypeCode.I8; break;
				case CILCode.Conv_r4: type = MosaTypeCode.R4; break;
				case CILCode.Conv_r8: type = MosaTypeCode.R8; break;
				case CILCode.Conv_u1: type = MosaTypeCode.U1; break;
				case CILCode.Conv_u2: type = MosaTypeCode.U2; break;
				case CILCode.Conv_u4: type = MosaTypeCode.U4; break;
				case CILCode.Conv_u8: type = MosaTypeCode.U8; break;
				case CILCode.Conv_r_un: type = MosaTypeCode.R8; break;
				default: throw new InvalidCompilerException();
			}
		}

		MosaTypeCode type;

		static void EmitConvI4(MosaType to, Value val, TransformContext ctx)
		{
			if (to.IsUI4)
			{
				ctx.EvaluationStack.Push(val);
				return;
			}
			var result = ctx.CreateVReg(to);
			if (to.IsR)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.FloatingPointToIntegerConversion)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsUnsigned || to.IsChar || to.IsBoolean)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveZeroExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsSigned)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveSignExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else
				throw new InvalidCompilerException();
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConvI8(MosaType to, Value val, TransformContext ctx)
		{
			if (to.IsUI8)
			{
				ctx.EvaluationStack.Push(val);
				return;
			}

			var result = ctx.CreateVReg(to);
			if (to.IsR)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.FloatingPointToIntegerConversion)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsUnsigned || to.IsChar || to.IsBoolean)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveZeroExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsSigned)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveSignExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else
				throw new InvalidCompilerException();
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConvN(MosaType to, Value val, TransformContext ctx)
		{
			if (to.IsN)
			{
				ctx.EvaluationStack.Push(val);
				return;
			}

			var result = ctx.CreateVReg(to);
			if (to.IsR)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.FloatingPointToIntegerConversion)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsUnsigned || to.IsChar || to.IsBoolean)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveZeroExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else if (to.IsSigned)
			{
				ctx.IRPointer.Append()
					.SetOpCode(IROpCodes.MoveSignExtended)
					.SetOperand1(val.ToOperand())
					.SetResult(result);
			}
			else
				throw new InvalidCompilerException();
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConvR4(MosaType to, Value val, TransformContext ctx)
		{
			if (to.IsR4)
			{
				ctx.EvaluationStack.Push(val);
				return;
			}
			else if (to.IsR8)
			{
				throw new NotImplementCompilerException();
			}

			var result = ctx.CreateVReg(to);
			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.IntegerToFloatConversion)
				.SetOperand1(val.ToOperand())
				.SetResult(result);
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConvR8(MosaType to, Value val, TransformContext ctx)
		{
			if (to.IsR8)
			{
				ctx.EvaluationStack.Push(val);
				return;
			}
			else if (to.IsR4)
			{
				throw new NotImplementCompilerException();
			}

			var result = ctx.CreateVReg(to);
			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.IntegerToFloatConversion)
				.SetOperand1(val.ToOperand())
				.SetResult(result);
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConvPtr(MosaType to, Value val, TransformContext ctx)
		{
			var result = ctx.CreateVReg(to);
			ctx.IRPointer.Append()
				.SetOpCode(IROpCodes.Move)
				.SetOperand1(val.ToOperand())
				.SetResult(result);
			ctx.EvaluationStack.Push(result);
		}

		static void EmitConv_r_un(Value val, TransformContext ctx)
		{
			// TODO: fix conv.r.un
			throw new NotImplementCompilerException();
		}

		public override void Parse(TransformContext ctx)
		{
			var val = ctx.EvaluationStack.Pop();
			if (Code == CILCode.Conv_r_un)
			{
				EmitConv_r_un(val, ctx);
				return;
			}

			var targetType = ctx.TypeSystem.GetTypeFromTypeCode(type);

			switch (val.Type.GetStackTypeCode())
			{
				case StackTypeCode.Int32:
					EmitConvI4(targetType, val, ctx);
					break;

				case StackTypeCode.Int64:
					EmitConvI8(targetType, val, ctx);
					break;

				case StackTypeCode.N:
					EmitConvN(targetType, val, ctx);
					break;

				case StackTypeCode.F:
					if (val.Type.IsR4)
						EmitConvR4(targetType, val, ctx);
					else
						EmitConvR8(targetType, val, ctx);
					break;

				case StackTypeCode.ManagedPointer:
				case StackTypeCode.UnmanagedPointer:
				case StackTypeCode.O:
					EmitConvPtr(targetType, val, ctx);
					break;
			}
		}
	}
}
