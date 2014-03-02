/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using System.Diagnostics;
using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.Common;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Transformations
{
	/* 
	 * This transformation does two things:
	 * 1. Promote local variables to virtual registers if compatible with register
	 * 2. Replace local variables with stack values if value must be stored in stack
	 * 
	 * If a variable is
	 * a) user value types, or
	 * b) long value in 32 bit arch, or
	 * c) obtained address using AddressOf,
	 * it must be stored on stack.
	 * 
	 * In addition, if a local variable is used in protected blocks (i.e. try/catch...),
	 * it must be stored to a stack location after every definition to ensure proper exception handling.
	 * 
	 */
	public class VRegPromotion
	{
		MethodBody body;
		bool is32Bits;
		Dictionary<Value, StackValue> stackLookup;
		Dictionary<LocalValue, Value> localLookup;
		Dictionary<Tuple<Value, int>, SSAValue> ssaLookup;

		public VRegPromotion(MethodBody body, bool is32Bits)
		{
			this.body = body;
			this.is32Bits = is32Bits;
			stackLookup = new Dictionary<Value, StackValue>();
			localLookup = new Dictionary<LocalValue, Value>();
			ssaLookup = new Dictionary<Tuple<Value, int>, SSAValue>();
		}

		bool MustStack(MosaType type)
		{
			return type.IsUserValueType || (is32Bits && type.IsUI8);
		}

		StackValue GetStack(Value value)
		{
			StackValue result;
			if (!stackLookup.TryGetValue(value, out result))
				result = stackLookup[value] = new StackValue(value.Type);
			return result;
		}

		void FindProtectedVariables()
		{
			// TODO: implement EH
		}

		void FindStackLocals()
		{
			foreach (var local in body.Locals)
			{
				if (MustStack(local.Type))
					GetStack(local);
			}

			foreach (var block in body.GetAllBlocks())
				foreach (var instr in block.Body)
				{
					if (instr.OpCode != IROpCodes.AddressOf)
						continue;

					ValueOperand operand = instr.Operand1 as ValueOperand;
					if (operand != null)
					{
						Value value = operand.Value;
						if (value is SSAValue)
							value = ((SSAValue)value).OriginValue;
						GetStack(value);
					}
				}
		}

		void InitLocalLookup()
		{
			foreach (var local in body.Locals)
			{
				if (stackLookup.ContainsKey(local))
					localLookup[local] = stackLookup[local];
				else
					localLookup[local] = body.CreateVReg(local.Type);
			}
		}

		SSAValue GetSSA(Value value, int version)
		{
			var key = Tuple.Create(value, version);
			SSAValue result;
			if (!ssaLookup.TryGetValue(key, out result))
				result = ssaLookup[key] = new SSAValue(value, version);
			return result;
		}

		Operand ConvertOperand(Operand operand)
		{
			if (operand is PhiOperand)
			{
				PhiOperand phi = (PhiOperand)operand;
				SSAValue value = (SSAValue)phi.Value;
				if (value.OriginValue is LocalValue)
					return PhiOperand.Create(GetSSA(localLookup[(LocalValue)value.OriginValue], value.Version), phi.SourceBlock);
				else if (stackLookup.ContainsKey(value.OriginValue))
					return PhiOperand.Create(GetSSA(stackLookup[value.OriginValue], value.Version), phi.SourceBlock);
			}
			else if (operand is ValueOperand)
			{
				Value value = ((ValueOperand)operand).Value;
				if (value is SSAValue)
				{
					SSAValue ssa = (SSAValue)value;
					if (ssa.OriginValue is LocalValue)
						return PhiOperand.Create(GetSSA(localLookup[(LocalValue)ssa.OriginValue], ssa.Version));
					else if (stackLookup.ContainsKey(ssa.OriginValue))
						return PhiOperand.Create(GetSSA(stackLookup[ssa.OriginValue], ssa.Version));
				}
				else
				{
					if (value is LocalValue)
						return localLookup[(LocalValue)value].ToOperand();
					else if (stackLookup.ContainsKey(value))
						return stackLookup[value].ToOperand();
				}
			}
			return operand;
		}

		Value ConvertValue(Value value)
		{
			if (value is SSAValue)
			{
				SSAValue ssa = (SSAValue)value;
				if (ssa.OriginValue is LocalValue)
					return GetSSA(localLookup[(LocalValue)ssa.OriginValue], ssa.Version);
				else if (stackLookup.ContainsKey(ssa.OriginValue))
					return GetSSA(stackLookup[ssa.OriginValue], ssa.Version);
			}
			else
			{
				if (value is LocalValue)
					return localLookup[(LocalValue)value];
				else if (stackLookup.ContainsKey(value))
					return stackLookup[value];
			}
			return value;
		}

		void ConvertVariables()
		{
			foreach (var block in body.GetAllBlocks())
				foreach (var instr in block.Body)
				{
					Debug.Assert(instr.Results.Count <= 1);
					if (instr.Results.Count > 0)
					{
						instr.Result = ConvertValue(instr.Result);
					}


					for (int i = 0; i < instr.Operands.Count; i++)
						instr.Operands[i] = ConvertOperand(instr.Operands[i]);
				}
		}

		public void Run()
		{
			FindProtectedVariables();
			FindStackLocals();
			InitLocalLookup();
			ConvertVariables();
		}
	}
}
