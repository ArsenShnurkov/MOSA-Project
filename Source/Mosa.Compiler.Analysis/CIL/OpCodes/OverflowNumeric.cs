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

namespace Mosa.Compiler.Analysis.CIL.OpCodes
{
	internal class OverflowNumeric : CILOpCode
	{
		public OverflowNumeric(CILCode code)
			: base(code)
		{
		}

		// Assume code is verificable
		static MosaType GetResultType(TypeSystem typeSystem, CILCode code, MosaType a, MosaType b)
		{
			if (a.IsPointer && b.IsPointer)
				return typeSystem.BuiltIn.I;
			else if (a.IsPointer || b.IsPointer)
			{
				Debug.Assert(a.Equals(b));
				return a;
			}
			else if (a.IsUI8 || b.IsUI8)
				return typeSystem.BuiltIn.I8;
			else if (a.IsN || b.IsN)
				return typeSystem.BuiltIn.I;
			else
				return typeSystem.BuiltIn.I4;
		}

		public override void Parse(TransformContext ctx)
		{
			var a = ctx.EvaluationStack.Pop();
			var b = ctx.EvaluationStack.Pop();
			var result = ctx.CreateVReg(GetResultType(ctx.TypeSystem, base.Code, a.Type, b.Type));

			ctx.IRPointer.Append()
				.SetOpCode(base.Code.ToIRArithmetic(result.Type))
				.SetOperand1(a.ToOperand())
				.SetOperand2(b.ToOperand())
				.SetResult(result);

			// TODO: Check overflow

			ctx.EvaluationStack.Push(result);
		}
	}
}
