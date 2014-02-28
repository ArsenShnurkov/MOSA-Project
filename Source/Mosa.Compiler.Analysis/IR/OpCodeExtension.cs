/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.CIL;
using Mosa.Compiler.Common;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.IR
{
	internal static class OpCodeExtension
	{
		public static IROpCode GetComparisonOpCode(this MosaType type)
		{
			return type.IsR ? (IROpCode)IROpCodes.FloatCompare : IROpCodes.IntegerCompare;
		}

		public static IROpCode GetMoveOpCode(this MosaType type)
		{
			if (type.IsI1 || type.IsI2)
				return IROpCodes.MoveSignExtended;

			else if (type.IsU1 || type.IsU2 || type.IsChar || type.IsBoolean)
				return IROpCodes.MoveZeroExtended;

			else
				return IROpCodes.Move;
		}

		public static IROpCode GetLoadOpCode(this MosaType type)
		{
			if (type.IsI1 || type.IsI2)
				return IROpCodes.LoadSignExtended;

			else if (type.IsU1 || type.IsU2 || type.IsChar || type.IsBoolean)
				return IROpCodes.LoadZeroExtended;

			else
				return IROpCodes.Load;
		}


		public static ConditionCode ToConditionCode(this CILCode code)
		{
			switch (code)
			{
				case CILCode.Beq:
				case CILCode.Beq_s:
				case CILCode.Ceq:
					return ConditionCode.Equal;

				case CILCode.Bne_un:
				case CILCode.Bne_un_s:
					return ConditionCode.NotEqual;

				case CILCode.Bgt:
				case CILCode.Bgt_s:
				case CILCode.Cgt:
					return ConditionCode.GreaterThan;

				case CILCode.Bge:
				case CILCode.Bge_s:
					return ConditionCode.GreaterOrEqual;

				case CILCode.Blt:
				case CILCode.Blt_s:
				case CILCode.Clt:
					return ConditionCode.LessThan;

				case CILCode.Ble:
				case CILCode.Ble_s:
					return ConditionCode.LessOrEqual;

				case CILCode.Bgt_un:
				case CILCode.Bgt_un_s:
				case CILCode.Cgt_un:
					return ConditionCode.UnsignedGreaterThan;

				case CILCode.Bge_un:
				case CILCode.Bge_un_s:
					return ConditionCode.UnsignedGreaterOrEqual;

				case CILCode.Blt_un:
				case CILCode.Blt_un_s:
				case CILCode.Clt_un:
					return ConditionCode.UnsignedLessThan;

				case CILCode.Ble_un:
				case CILCode.Ble_un_s:
					return ConditionCode.UnsignedLessOrEqual;
			}
			throw new InvalidCompilerException();
		}

		public static IROpCode ToIRArithmetic(this CILCode code, MosaType type)
		{
			switch (code)
			{
				case CILCode.Add:
					return type.IsR ? (IROpCode)IROpCodes.AddF : IROpCodes.AddS;
				case CILCode.Add_ovf:
					return IROpCodes.AddS;
				case CILCode.Add_ovf_un:
					return IROpCodes.AddU;

				case CILCode.Sub:
					return type.IsR ? (IROpCode)IROpCodes.SubF : IROpCodes.SubS;
				case CILCode.Sub_ovf:
					return IROpCodes.SubS;
				case CILCode.Sub_ovf_un:
					return IROpCodes.SubU;

				case CILCode.Mul:
					return type.IsR ? (IROpCode)IROpCodes.MulF : IROpCodes.MulS;
				case CILCode.Mul_ovf:
					return IROpCodes.MulS;
				case CILCode.Mul_ovf_un:
					return IROpCodes.MulU;

				case CILCode.Div:
					return type.IsR ? (IROpCode)IROpCodes.DivF : IROpCodes.DivS;
				case CILCode.Div_un:
					return IROpCodes.DivU;

				case CILCode.Rem:
					return type.IsR ? (IROpCode)IROpCodes.RemF : IROpCodes.RemS;
				case CILCode.Rem_un:
					return IROpCodes.RemU;
			}
			throw new InvalidCompilerException();
		}

		public static int? GetStlocIndex(this MosaInstruction instr)
		{
			switch ((CILCode)instr.OpCode)
			{
				case CILCode.Stloc_0: return 0;
				case CILCode.Stloc_1: return 1;
				case CILCode.Stloc_2: return 2;
				case CILCode.Stloc_3: return 3;
				case CILCode.Stloc:
				case CILCode.Stloc_s: return (int)instr.Operand;
			}
			return null;
		}
	}
}
