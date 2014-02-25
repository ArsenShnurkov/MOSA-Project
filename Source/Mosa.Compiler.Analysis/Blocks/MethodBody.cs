/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using System.Text;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Blocks
{
	public class MethodBody : ScopeBlock
	{
		public MosaMethod Method { get; private set; }

		public IList<StackValue> Parameters { get; private set; }

		public IList<LocalValue> Locals { get; private set; }

		public BasicBlock EntryBlock { get; private set; }

		public BasicBlock AbnormalBlock { get; private set; }

		public BasicBlock ReturnBlock { get; private set; }

		public int VRegNumber { get; private set; }

		public MethodBody(MosaMethod method)
			: base(BlockType.Method)
		{
			Method = method;
			BuildStackValues();

			EntryBlock = new BasicBlock(0) { IsTechnicalBlock = true };
			EntryBlock.CILInstructions.Add(new MosaInstruction(-1, 0, null, null, null));

			ReturnBlock = new BasicBlock(-1) { IsTechnicalBlock = true };
			ReturnBlock.CILInstructions.Add(new MosaInstruction(-1, 0, null, null, null));

			AbnormalBlock = new BasicBlock(-2) { IsTechnicalBlock = true };
			AbnormalBlock.CILInstructions.Add(new MosaInstruction(-1, 0, null, null, null));
		}

		void BuildStackValues()
		{
			Parameters = new List<StackValue>();
			if (Method.HasThis && !Method.HasExplicitThis)
			{
				MosaType selfType = Method.DeclaringType;
				if (Method.DeclaringType.IsValueType)
					selfType = selfType.ToManagedPointer();
				Parameters.Add(new StackValue(selfType));
			}
			foreach (var param in Method.Signature.Parameters)
			{
				MosaType parType = param.Type;
				if (parType.IsEnum)
					parType = parType.GetEnumUnderlyingType();

				Parameters.Add(new StackValue(parType));
			}

			Locals = new List<LocalValue>();
			foreach (var variable in Method.LocalVariables)
			{
				MosaType varType = variable.Type;
				if (varType.IsEnum)
					varType = varType.GetEnumUnderlyingType();

				bool canRegister = true;
				//if (Method.ExceptionBlocks.Count > 0)
				//    canRegister = false;
				//if (varType.IsUserValueType)
				//    canRegister = false;

				Locals.Add(new LocalValue(variable, varType, canRegister));
			}
		}
		
		public VRegValue CreateVReg(MosaType type)
		{
			return new VRegValue(type, VRegNumber++);
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			foreach (var i in EntryBlock.ReversePostOrder())
				result.AppendLine(i.ToString());
			return result.ToString();
		}
	}
}
