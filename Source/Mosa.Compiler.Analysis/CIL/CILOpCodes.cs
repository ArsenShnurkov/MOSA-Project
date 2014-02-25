/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using Mosa.Compiler.Analysis.CIL.OpCodes;

namespace Mosa.Compiler.Analysis.CIL
{
	internal class CILOpCodes
	{
		static CILOpCodes()
		{
			Initialize();
		}

		public static readonly CILOpCode[] OpCodes = new CILOpCode[0x120];

		static void Initialize()
		{
			/* 0x000 */
			OpCodes[(int)CILCode.Nop] = new Nop();
			/* 0x001 */
			//OpCodes[(int)CILCode.Break] = new Break(CILCode.Break);
			/* 0x002 */
			OpCodes[(int)CILCode.Ldarg_0] = new Ldarg(CILCode.Ldarg_0);
			/* 0x003 */
			OpCodes[(int)CILCode.Ldarg_1] = new Ldarg(CILCode.Ldarg_1);
			/* 0x004 */
			OpCodes[(int)CILCode.Ldarg_2] = new Ldarg(CILCode.Ldarg_2);
			/* 0x005 */
			OpCodes[(int)CILCode.Ldarg_3] = new Ldarg(CILCode.Ldarg_3);
			/* 0x006 */
			OpCodes[(int)CILCode.Ldloc_0] = new Ldloc(CILCode.Ldloc_0);
			/* 0x007 */
			OpCodes[(int)CILCode.Ldloc_1] = new Ldloc(CILCode.Ldloc_1);
			/* 0x008 */
			OpCodes[(int)CILCode.Ldloc_2] = new Ldloc(CILCode.Ldloc_2);
			/* 0x009 */
			OpCodes[(int)CILCode.Ldloc_3] = new Ldloc(CILCode.Ldloc_3);
			/* 0x00A */
			OpCodes[(int)CILCode.Stloc_0] = new Stloc(CILCode.Stloc_0);
			/* 0x00B */
			OpCodes[(int)CILCode.Stloc_1] = new Stloc(CILCode.Stloc_1);
			/* 0x00C */
			OpCodes[(int)CILCode.Stloc_2] = new Stloc(CILCode.Stloc_2);
			/* 0x00D */
			OpCodes[(int)CILCode.Stloc_3] = new Stloc(CILCode.Stloc_3);
			/* 0x00E */
			OpCodes[(int)CILCode.Ldarg_s] = new Ldarg(CILCode.Ldarg_s);
			/* 0x00F */
			//OpCodes[(int)CILCode.Ldarga_s] = new Ldarga(CILCode.Ldarga_s);
			/* 0x010 */
			OpCodes[(int)CILCode.Starg_s] = new Starg(CILCode.Starg_s);
			/* 0x011 */
			OpCodes[(int)CILCode.Ldloc_s] = new Ldloc(CILCode.Ldloc_s);
			/* 0x012 */
			//OpCodes[(int)CILCode.Ldloca_s] = new Ldloca(CILCode.Ldloca_s);
			/* 0x013 */
			OpCodes[(int)CILCode.Stloc_s] = new Stloc(CILCode.Stloc_s);
			/* 0x014 */
			OpCodes[(int)CILCode.Ldnull] = new Ldc(CILCode.Ldnull);
			/* 0x015 */
			OpCodes[(int)CILCode.Ldc_i4_m1] = new Ldc(CILCode.Ldc_i4_m1);
			/* 0x016 */
			OpCodes[(int)CILCode.Ldc_i4_0] = new Ldc(CILCode.Ldc_i4_0);
			/* 0x017 */
			OpCodes[(int)CILCode.Ldc_i4_1] = new Ldc(CILCode.Ldc_i4_1);
			/* 0x018 */
			OpCodes[(int)CILCode.Ldc_i4_2] = new Ldc(CILCode.Ldc_i4_2);
			/* 0x019 */
			OpCodes[(int)CILCode.Ldc_i4_3] = new Ldc(CILCode.Ldc_i4_3);
			/* 0x01A */
			OpCodes[(int)CILCode.Ldc_i4_4] = new Ldc(CILCode.Ldc_i4_4);
			/* 0x01B */
			OpCodes[(int)CILCode.Ldc_i4_5] = new Ldc(CILCode.Ldc_i4_5);
			/* 0x01C */
			OpCodes[(int)CILCode.Ldc_i4_6] = new Ldc(CILCode.Ldc_i4_6);
			/* 0x01D */
			OpCodes[(int)CILCode.Ldc_i4_7] = new Ldc(CILCode.Ldc_i4_7);
			/* 0x01E */
			OpCodes[(int)CILCode.Ldc_i4_8] = new Ldc(CILCode.Ldc_i4_8);
			/* 0x01F */
			OpCodes[(int)CILCode.Ldc_i4_s] = new Ldc(CILCode.Ldc_i4_s);
			/* 0x020 */
			OpCodes[(int)CILCode.Ldc_i4] = new Ldc(CILCode.Ldc_i4);
			/* 0x021 */
			OpCodes[(int)CILCode.Ldc_i8] = new Ldc(CILCode.Ldc_i8);
			/* 0x022 */
			OpCodes[(int)CILCode.Ldc_r4] = new Ldc(CILCode.Ldc_r4);
			/* 0x023 */
			OpCodes[(int)CILCode.Ldc_r8] = new Ldc(CILCode.Ldc_r8);
			/* 0x024 is undefined */
			/* 0x025 */
			OpCodes[(int)CILCode.Dup] = new Dup();
			/* 0x026 */
			OpCodes[(int)CILCode.Pop] = new Pop();
			/* 0x027 */
			//OpCodes[(int)CILCode.Jmp] = new Jump(CILCode.Jmp);
			/* 0x028 */
			//OpCodes[(int)CILCode.Call] = new Call(CILCode.Call);
			/* 0x029 */
			//OpCodes[(int)CILCode.Calli] = new Calli(CILCode.Calli);
			/* 0x02A */
			OpCodes[(int)CILCode.Ret] = new Ret();
			/* 0x02B */
			OpCodes[(int)CILCode.Br_s] = new Unconditional(CILCode.Br_s);
			/* 0x02C */
			OpCodes[(int)CILCode.Brfalse_s] = new UnaryConditional(CILCode.Brfalse_s);
			/* 0x02D */
			OpCodes[(int)CILCode.Brtrue_s] = new UnaryConditional(CILCode.Brtrue_s);
			/* 0x02E */
			OpCodes[(int)CILCode.Beq_s] = new BinaryConditional(CILCode.Beq_s);
			/* 0x02F */
			OpCodes[(int)CILCode.Bge_s] = new BinaryConditional(CILCode.Bge_s);
			/* 0x030 */
			OpCodes[(int)CILCode.Bgt_s] = new BinaryConditional(CILCode.Bgt_s);
			/* 0x031 */
			OpCodes[(int)CILCode.Ble_s] = new BinaryConditional(CILCode.Ble_s);
			/* 0x032 */
			OpCodes[(int)CILCode.Blt_s] = new BinaryConditional(CILCode.Blt_s);
			/* 0x033 */
			OpCodes[(int)CILCode.Bne_un_s] = new BinaryConditional(CILCode.Bne_un_s);
			/* 0x034 */
			OpCodes[(int)CILCode.Bge_un_s] = new BinaryConditional(CILCode.Bge_un_s);
			/* 0x035 */
			OpCodes[(int)CILCode.Bgt_un_s] = new BinaryConditional(CILCode.Bgt_un_s);
			/* 0x036 */
			OpCodes[(int)CILCode.Ble_un_s] = new BinaryConditional(CILCode.Ble_un_s);
			/* 0x037 */
			OpCodes[(int)CILCode.Blt_un_s] = new BinaryConditional(CILCode.Blt_un_s);
			/* 0x038 */
			OpCodes[(int)CILCode.Br] = new Unconditional(CILCode.Br);
			/* 0x039 */
			OpCodes[(int)CILCode.Brfalse] = new UnaryConditional(CILCode.Brfalse);
			/* 0x03A */
			OpCodes[(int)CILCode.Brtrue] = new UnaryConditional(CILCode.Brtrue);
			/* 0x03B */
			OpCodes[(int)CILCode.Beq] = new BinaryConditional(CILCode.Beq);
			/* 0x03C */
			OpCodes[(int)CILCode.Bge] = new BinaryConditional(CILCode.Bge);
			/* 0x03D */
			OpCodes[(int)CILCode.Bgt] = new BinaryConditional(CILCode.Bgt);
			/* 0x03E */
			OpCodes[(int)CILCode.Ble] = new BinaryConditional(CILCode.Ble);
			/* 0x03F */
			OpCodes[(int)CILCode.Blt] = new BinaryConditional(CILCode.Blt);
			/* 0x040 */
			OpCodes[(int)CILCode.Bne_un] = new BinaryConditional(CILCode.Bne_un);
			/* 0x041 */
			OpCodes[(int)CILCode.Bge_un] = new BinaryConditional(CILCode.Bge_un);
			/* 0x042 */
			OpCodes[(int)CILCode.Bgt_un] = new BinaryConditional(CILCode.Bgt_un);
			/* 0x043 */
			OpCodes[(int)CILCode.Ble_un] = new BinaryConditional(CILCode.Ble_un);
			/* 0x044 */
			OpCodes[(int)CILCode.Blt_un] = new BinaryConditional(CILCode.Blt_un);
			/* 0x045 */
			//OpCodes[(int)CILCode.Switch] = new Switch(CILCode.Switch);
			/* 0x046 */
			//OpCodes[(int)CILCode.Ldind_i1] = new Ldobj(CILCode.Ldind_i1);
			/* 0x047 */
			//OpCodes[(int)CILCode.Ldind_u1] = new Ldobj(CILCode.Ldind_u1);
			/* 0x048 */
			//OpCodes[(int)CILCode.Ldind_i2] = new Ldobj(CILCode.Ldind_i2);
			/* 0x049 */
			//OpCodes[(int)CILCode.Ldind_u2] = new Ldobj(CILCode.Ldind_u2);
			/* 0x04A */
			//OpCodes[(int)CILCode.Ldind_i4] = new Ldobj(CILCode.Ldind_i4);
			/* 0x04B */
			//OpCodes[(int)CILCode.Ldind_u4] = new Ldobj(CILCode.Ldind_u4);
			/* 0x04C */
			//OpCodes[(int)CILCode.Ldind_i8] = new Ldobj(CILCode.Ldind_i8);
			/* 0x04D */
			//OpCodes[(int)CILCode.Ldind_i] = new Ldobj(CILCode.Ldind_i);
			/* 0x04E */
			//OpCodes[(int)CILCode.Ldind_r4] = new Ldobj(CILCode.Ldind_r4);
			/* 0x04F */
			//OpCodes[(int)CILCode.Ldind_r8] = new Ldobj(CILCode.Ldind_r8);
			/* 0x050 */
			//OpCodes[(int)CILCode.Ldind_ref] = new Ldobj(CILCode.Ldind_ref);
			/* 0x051 */
			//OpCodes[(int)CILCode.Stind_ref] = new Stobj(CILCode.Stind_ref);
			/* 0x052 */
			//OpCodes[(int)CILCode.Stind_i1] = new Stobj(CILCode.Stind_i1);
			/* 0x053 */
			//OpCodes[(int)CILCode.Stind_i2] = new Stobj(CILCode.Stind_i2);
			/* 0x054 */
			//OpCodes[(int)CILCode.Stind_i4] = new Stobj(CILCode.Stind_i4);
			/* 0x055 */
			//OpCodes[(int)CILCode.Stind_i8] = new Stobj(CILCode.Stind_i8);
			/* 0x056 */
			//OpCodes[(int)CILCode.Stind_r4] = new Stobj(CILCode.Stind_r4);
			/* 0x057 */
			//OpCodes[(int)CILCode.Stind_r8] = new Stobj(CILCode.Stind_r8);
			/* 0x058 */
			OpCodes[(int)CILCode.Add] = new Arithmetic(CILCode.Add);
			/* 0x059 */
			OpCodes[(int)CILCode.Sub] = new Arithmetic(CILCode.Sub);
			/* 0x05A */
			OpCodes[(int)CILCode.Mul] = new Arithmetic(CILCode.Mul);
			/* 0x05B */
			OpCodes[(int)CILCode.Div] = new Arithmetic(CILCode.Div);
			/* 0x05C */
			//OpCodes[(int)CILCode.Div_un] = new BinaryLogic(CILCode.Div_un);
			/* 0x05D */
			OpCodes[(int)CILCode.Rem] = new Arithmetic(CILCode.Rem);
			/* 0x05E */
			//OpCodes[(int)CILCode.Rem_un] = new BinaryLogic(CILCode.Rem_un);
			/* 0x05F */
			//OpCodes[(int)CILCode.And] = new BinaryLogic(CILCode.And);
			/* 0x060 */
			//OpCodes[(int)CILCode.Or] = new BinaryLogic(CILCode.Or);
			/* 0x061 */
			//OpCodes[(int)CILCode.Xor] = new BinaryLogic(CILCode.Xor);
			/* 0x062 */
			//OpCodes[(int)CILCode.Shl] = new Shift(CILCode.Shl);
			/* 0x063 */
			//OpCodes[(int)CILCode.Shr] = new Shift(CILCode.Shr);
			/* 0x064 */
			//OpCodes[(int)CILCode.Shr_un] = new Shift(CILCode.Shr_un);
			/* 0x065 */
			//OpCodes[(int)CILCode.Neg] = new Neg(CILCode.Neg);
			/* 0x066 */
			//OpCodes[(int)CILCode.Not] = new Not(CILCode.Not);
			/* 0x067 */
			//OpCodes[(int)CILCode.Conv_i1] = new Conversion(CILCode.Conv_i1);
			/* 0x068 */
			//OpCodes[(int)CILCode.Conv_i2] = new Conversion(CILCode.Conv_i2);
			/* 0x069 */
			//OpCodes[(int)CILCode.Conv_i4] = new Conversion(CILCode.Conv_i4);
			/* 0x06A */
			//OpCodes[(int)CILCode.Conv_i8] = new Conversion(CILCode.Conv_i8);
			/* 0x06B */
			//OpCodes[(int)CILCode.Conv_r4] = new Conversion(CILCode.Conv_r4);
			/* 0x06C */
			//OpCodes[(int)CILCode.Conv_r8] = new Conversion(CILCode.Conv_r8);
			/* 0x06D */
			//OpCodes[(int)CILCode.Conv_u4] = new Conversion(CILCode.Conv_u4);
			/* 0x06E */
			//OpCodes[(int)CILCode.Conv_u8] = new Conversion(CILCode.Conv_u8);
			/* 0x06F */
			//OpCodes[(int)CILCode.Callvirt] = new Callvirt(CILCode.Callvirt);
			/* 0x070 */
			//OpCodes[(int)CILCode.Cpobj] = new Cpobj(CILCode.Cpobj);
			/* 0x071 */
			//OpCodes[(int)CILCode.Ldobj] = new Ldobj(CILCode.Ldobj);
			/* 0x072 */
			OpCodes[(int)CILCode.Ldstr] = new Ldc(CILCode.Ldstr);
			/* 0x073 */
			//OpCodes[(int)CILCode.Newobj] = new Newobj(CILCode.Newobj);
			/* 0x074 */
			//OpCodes[(int)CILCode.Castclass] = new Castclass(CILCode.Castclass);
			/* 0x075 */
			//OpCodes[(int)CILCode.Isinst] = new IsInst(CILCode.Isinst);
			/* 0x076 */
			//OpCodes[(int)CILCode.Conv_r_un] = new Conversion(CILCode.Conv_r_un);
			/* Opcodes 0x077-0x078 undefined */
			/* 0x079 */
			//OpCodes[(int)CILCode.Unbox] = new Unbox(CILCode.Unbox);
			/* 0x07A */
			//OpCodes[(int)CILCode.Throw] = new Throw(CILCode.Throw);
			/* 0x07B */
			//OpCodes[(int)CILCode.Ldfld] = new Ldfld(CILCode.Ldfld);
			/* 0x07C */
			//OpCodes[(int)CILCode.Ldflda] = new Ldflda(CILCode.Ldflda);
			/* 0x07D */
			//OpCodes[(int)CILCode.Stfld] = new Stfld(CILCode.Stfld);
			/* 0x07E */
			//OpCodes[(int)CILCode.Ldsfld] = new Ldsfld(CILCode.Ldsfld);
			/* 0x07F */
			//OpCodes[(int)CILCode.Ldsflda] = new Ldsflda(CILCode.Ldsflda);
			/* 0x080 */
			//OpCodes[(int)CILCode.Stsfld] = new Stsfld(CILCode.Stsfld);
			/* 0x081 */
			//OpCodes[(int)CILCode.Stobj] = new Stobj(CILCode.Stobj);
			/* 0x082 */
			//OpCodes[(int)CILCode.Conv_ovf_i1_un] = new Conversion(CILCode.Conv_ovf_i1_un);
			/* 0x083 */
			//OpCodes[(int)CILCode.Conv_ovf_i2_un] = new Conversion(CILCode.Conv_ovf_i2_un);
			/* 0x084 */
			//OpCodes[(int)CILCode.Conv_ovf_i4_un] = new Conversion(CILCode.Conv_ovf_i4_un);
			/* 0x085 */
			//OpCodes[(int)CILCode.Conv_ovf_i8_un] = new Conversion(CILCode.Conv_ovf_i8_un);
			/* 0x086 */
			//OpCodes[(int)CILCode.Conv_ovf_u1_un] = new Conversion(CILCode.Conv_ovf_u1_un);
			/* 0x087 */
			//OpCodes[(int)CILCode.Conv_ovf_u2_un] = new Conversion(CILCode.Conv_ovf_u2_un);
			/* 0x088 */
			//OpCodes[(int)CILCode.Conv_ovf_u4_un] = new Conversion(CILCode.Conv_ovf_u4_un);
			/* 0x089 */
			//OpCodes[(int)CILCode.Conv_ovf_u8_un] = new Conversion(CILCode.Conv_ovf_u8_un);
			/* 0x08A */
			//OpCodes[(int)CILCode.Conv_ovf_i_un] = new Conversion(CILCode.Conv_ovf_i_un);
			/* 0x08B */
			//OpCodes[(int)CILCode.Conv_ovf_u_un] = new Conversion(CILCode.Conv_ovf_u_un);
			/* 0x08C */
			//OpCodes[(int)CILCode.Box] = new Box(CILCode.Box);
			/* 0x08D */
			//OpCodes[(int)CILCode.Newarr] = new Newarr(CILCode.Newarr);
			/* 0x08E */
			//OpCodes[(int)CILCode.Ldlen] = new Ldlen(CILCode.Ldlen);
			/* 0x08F */
			//OpCodes[(int)CILCode.Ldelema] = new Ldelema(CILCode.Ldelema);
			/* 0x090 */
			//OpCodes[(int)CILCode.Ldelem_i1] = new Ldelem(CILCode.Ldelem_i1);
			/* 0x091 */
			//OpCodes[(int)CILCode.Ldelem_u1] = new Ldelem(CILCode.Ldelem_u1);
			/* 0x092 */
			//OpCodes[(int)CILCode.Ldelem_i2] = new Ldelem(CILCode.Ldelem_i2);
			/* 0x093 */
			//OpCodes[(int)CILCode.Ldelem_u2] = new Ldelem(CILCode.Ldelem_u2);
			/* 0x094 */
			//OpCodes[(int)CILCode.Ldelem_i4] = new Ldelem(CILCode.Ldelem_i4);
			/* 0x095 */
			//OpCodes[(int)CILCode.Ldelem_u4] = new Ldelem(CILCode.Ldelem_u4);
			/* 0x096 */
			//OpCodes[(int)CILCode.Ldelem_i8] = new Ldelem(CILCode.Ldelem_i8);
			/* 0x097 */
			//OpCodes[(int)CILCode.Ldelem_i] = new Ldelem(CILCode.Ldelem_i);
			/* 0x098 */
			//OpCodes[(int)CILCode.Ldelem_r4] = new Ldelem(CILCode.Ldelem_r4);
			/* 0x099 */
			//OpCodes[(int)CILCode.Ldelem_r8] = new Ldelem(CILCode.Ldelem_r8);
			/* 0x09A */
			//OpCodes[(int)CILCode.Ldelem_ref] = new Ldelem(CILCode.Ldelem_ref);
			/* 0x09B */
			//OpCodes[(int)CILCode.Stelem_i] = new Stelem(CILCode.Stelem_i);
			/* 0x09C */
			//OpCodes[(int)CILCode.Stelem_i1] = new Stelem(CILCode.Stelem_i1);
			/* 0x09D */
			//OpCodes[(int)CILCode.Stelem_i2] = new Stelem(CILCode.Stelem_i2);
			/* 0x09E */
			//OpCodes[(int)CILCode.Stelem_i4] = new Stelem(CILCode.Stelem_i4);
			/* 0x09F */
			//OpCodes[(int)CILCode.Stelem_i8] = new Stelem(CILCode.Stelem_i8);
			/* 0x0A0 */
			//OpCodes[(int)CILCode.Stelem_r4] = new Stelem(CILCode.Stelem_r4);
			/* 0x0A1 */
			//OpCodes[(int)CILCode.Stelem_r8] = new Stelem(CILCode.Stelem_r8);
			/* 0x0A2 */
			//OpCodes[(int)CILCode.Stelem_ref] = new Stelem(CILCode.Stelem_ref);
			/* 0x0A3 */
			//OpCodes[(int)CILCode.Ldelem] = new Ldelem(CILCode.Ldelem);
			/* 0x0A4 */
			//OpCodes[(int)CILCode.Stelem] = new Stelem(CILCode.Stelem);
			/* 0x0A5 */
			//OpCodes[(int)CILCode.Unbox_any] = new UnboxAny(CILCode.Unbox_any);
			/* Opcodes 0x0A6-0x0B2 are undefined */
			/* 0x0B3 */
			//OpCodes[(int)CILCode.Conv_ovf_i1] = new Conversion(CILCode.Conv_ovf_i1);
			/* 0x0B4 */
			//OpCodes[(int)CILCode.Conv_ovf_u1] = new Conversion(CILCode.Conv_ovf_u1);
			/* 0x0B5 */
			//OpCodes[(int)CILCode.Conv_ovf_i2] = new Conversion(CILCode.Conv_ovf_i2);
			/* 0x0B6 */
			//OpCodes[(int)CILCode.Conv_ovf_u2] = new Conversion(CILCode.Conv_ovf_u2);
			/* 0x0B7 */
			//OpCodes[(int)CILCode.Conv_ovf_i4] = new Conversion(CILCode.Conv_ovf_i4);
			/* 0x0B8 */
			//OpCodes[(int)CILCode.Conv_ovf_u4] = new Conversion(CILCode.Conv_ovf_u4);
			/* 0x0B9 */
			//OpCodes[(int)CILCode.Conv_ovf_i8] = new Conversion(CILCode.Conv_ovf_i8);
			/* 0x0BA */
			//OpCodes[(int)CILCode.Conv_ovf_u8] = new Conversion(CILCode.Conv_ovf_u8);
			/* Opcodes 0x0BB-0x0C1 are undefined */
			/* 0x0C2 */
			//OpCodes[(int)CILCode.Refanyval] = new Refanyval(CILCode.Refanyval);
			/* 0x0C3 */
			//OpCodes[(int)CILCode.Ckfinite] = new UnaryArithmetic(CILCode.Ckfinite);
			/* Opcodes 0x0C4-0x0C5 are undefined */
			/* 0x0C6 */
			//OpCodes[(int)CILCode.Mkrefany] = new Mkrefany(CILCode.Mkrefany);
			/* Opcodes 0x0C7-0x0CF are reserved */
			/* 0x0D0 */
			//OpCodes[(int)CILCode.Ldtoken] = new Ldtoken(CILCode.Ldtoken);
			/* 0x0D1 */
			//OpCodes[(int)CILCode.Conv_u2] = new Conversion(CILCode.Conv_u2);
			/* 0x0D2 */
			//OpCodes[(int)CILCode.Conv_u1] = new Conversion(CILCode.Conv_u1);
			/* 0x0D3 */
			//OpCodes[(int)CILCode.Conv_i] = new Conversion(CILCode.Conv_i);
			/* 0x0D4 */
			//OpCodes[(int)CILCode.Conv_ovf_i] = new Conversion(CILCode.Conv_ovf_i);
			/* 0x0D5 */
			//OpCodes[(int)CILCode.Conv_ovf_u] = new Conversion(CILCode.Conv_ovf_u);
			/* 0x0D6 */
			//OpCodes[(int)CILCode.Add_ovf] = new ArithmeticOverflow(CILCode.Add_ovf);
			/* 0x0D7 */
			//OpCodes[(int)CILCode.Add_ovf_un] = new ArithmeticOverflow(CILCode.Add_ovf_un);
			/* 0x0D8 */
			//OpCodes[(int)CILCode.Mul_ovf] = new ArithmeticOverflow(CILCode.Mul_ovf);
			/* 0x0D9 */
			//OpCodes[(int)CILCode.Mul_ovf_un] = new ArithmeticOverflow(CILCode.Mul_ovf_un);
			/* 0x0DA */
			//OpCodes[(int)CILCode.Sub_ovf] = new ArithmeticOverflow(CILCode.Sub_ovf);
			/* 0x0DB */
			//OpCodes[(int)CILCode.Sub_ovf_un] = new ArithmeticOverflow(CILCode.Sub_ovf_un);
			/* 0x0DC */
			//OpCodes[(int)CILCode.Endfinally] = new EndFinally(CILCode.Endfinally);
			/* 0x0DD */
			//OpCodes[(int)CILCode.Leave] = new Leave(CILCode.Leave);
			/* 0x0DE */
			//OpCodes[(int)CILCode.Leave_s] = new Leave(CILCode.Leave_s);
			/* 0x0DF */
			//OpCodes[(int)CILCode.Stind_i] = new Stobj(CILCode.Stind_i);
			/* 0x0E0 */
			//OpCodes[(int)CILCode.Conv_u] = new Conversion(CILCode.Conv_u);
			/* Opcodes 0xE1-0xFF are reserved */
			/* 0x100 */
			//OpCodes[(int)CILCode.Arglist] = new Arglist(CILCode.Arglist);
			/* 0x101 */
			OpCodes[(int)CILCode.Ceq] = new Compare(CILCode.Ceq);
			/* 0x102 */
			OpCodes[(int)CILCode.Cgt] = new Compare(CILCode.Cgt);
			/* 0x103 */
			OpCodes[(int)CILCode.Cgt_un] = new Compare(CILCode.Cgt_un);
			/* 0x104 */
			OpCodes[(int)CILCode.Clt] = new Compare(CILCode.Clt);
			/* 0x105 */
			OpCodes[(int)CILCode.Clt_un] = new Compare(CILCode.Clt_un);
			/* 0x106 */
			//OpCodes[(int)CILCode.Ldftn] = new Ldftn(CILCode.Ldftn);
			/* 0x107 */
			//OpCodes[(int)CILCode.Ldvirtftn] = new Ldvirtftn(CILCode.Ldvirtftn);
			/* Opcode 0x108 is undefined. */
			/* 0x109 */
			OpCodes[(int)CILCode.Ldarg] = new Ldarg(CILCode.Ldarg);
			/* 0x10A */
			//OpCodes[(int)CILCode.Ldarga] = new Ldarga(CILCode.Ldarga);
			/* 0x10B */
			OpCodes[(int)CILCode.Starg] = new Starg(CILCode.Starg);
			/* 0x10C */
			OpCodes[(int)CILCode.Ldloc] = new Ldloc(CILCode.Ldloc);
			/* 0x10D */
			//OpCodes[(int)CILCode.Ldloca] = new Ldloca(CILCode.Ldloca);
			/* 0x10E */
			OpCodes[(int)CILCode.Stloc] = new Stloc(CILCode.Stloc);
			/* 0x10F */
			//OpCodes[(int)CILCode.Localalloc] = new Localalloc(CILCode.Localalloc);
			/* Opcode 0x110 is undefined */
			/* 0x111 */
			//OpCodes[(int)CILCode.Endfilter] = new EndFilter(CILCode.Endfilter);
			/* 0x112 */
			//OpCodes[(int)CILCode.PreUnaligned] = new UnalignedPrefix(CILCode.PreUnaligned);
			/* 0x113 */
			//OpCodes[(int)CILCode.PreVolatile] = new VolatilePrefix(CILCode.PreVolatile);
			/* 0x114 */
			//OpCodes[(int)CILCode.PreTail] = new TailPrefix(CILCode.PreTail);
			/* 0x115 */
			//OpCodes[(int)CILCode.InitObj] = new InitObj(CILCode.InitObj);
			/* 0x116 */
			//OpCodes[(int)CILCode.PreConstrained] = new ConstrainedPrefix(CILCode.PreConstrained);
			/* 0x117 */
			//OpCodes[(int)CILCode.Cpblk] = new Cpblk(CILCode.Cpblk);
			/* 0x118 */
			//OpCodes[(int)CILCode.Initblk] = new Initblk(CILCode.Initblk);
			/* 0x119 */
			//OpCodes[(int)CILCode.PreNo] = new NoPrefix(CILCode.PreNo);
			/* 0x11A */
			//OpCodes[(int)CILCode.Rethrow] = new Rethrow(CILCode.Rethrow);
			/* Opcode 0x11B is undefined */
			/* 0x11C */
			//OpCodes[(int)CILCode.Sizeof] = new Sizeof(CILCode.Sizeof);
			/* 0x11D */
			//OpCodes[(int)CILCode.Refanytype] = new Refanytype(CILCode.Refanytype);
			/* 0x11E */
			//OpCodes[(int)CILCode.PreReadOnly] = new ReadOnlyPrefix(CILCode.PreReadOnly);
		}
	}
}
