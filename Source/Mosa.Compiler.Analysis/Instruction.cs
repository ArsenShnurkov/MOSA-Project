/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Text;
using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.IR.Operands;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis
{
	public class Instruction
	{
		internal Instruction()
		{
		}

		public uint CILOffset { get; set; }

		public BasicBlock Parent { get; set; }

		public OpCode OpCode { get; set; }

		Operand operand1;
		public Operand Operand1
		{
			get { return operand1; }
			set
			{
				SetUsage(this.operand1, value);
				this.operand1 = value;
			}
		}

		Operand operand2;
		public Operand Operand2
		{
			get { return operand2; }
			set
			{
				SetUsage(this.operand2, value);
				this.operand2 = value;
			}
		}

		void SetUsage(Operand originalVal, Operand newVal)
		{
			// Remove
			if (originalVal is ValueOperand)
			{
				((ValueOperand)originalVal).Value.Usages.Remove(this);
			}
			else if (originalVal is PhiOperand)
			{
				foreach (var val in ((PhiOperand)originalVal).Versions)
					val.Usages.Remove(this);
			}
			else if (originalVal is CallOperand)
			{
				foreach (var val in ((CallOperand)originalVal).Arguments)
					val.Usages.Remove(this);
			}

			// Add
			if (newVal is ValueOperand)
			{
				((ValueOperand)newVal).Value.Usages.Add(this);
			}
			else if (newVal is PhiOperand)
			{
				foreach (var val in ((PhiOperand)newVal).Versions)
					val.Usages.Add(this);
			}
			else if (originalVal is CallOperand)
			{
				foreach (var val in ((CallOperand)newVal).Arguments)
					val.Usages.Add(this);
			}

		}

		Value result;
		public Value Result
		{
			get { return result; }
			set
			{
				if (value != null)
				{
					if (value.Definition != null)
					{
#if DEBUG
						throw new CompilerException("Attempted to violate SSA property.");
#else
						value.Definition.Result = null;
#endif
					}
					value.Definition = this;
				}
				if (result != null)
					result.Definition = null;
				result = value;
			}
		}

		public object Extra { get; set; }

		public void Clear()
		{
			OpCode = null;
			Operand1 = null;
			Operand2 = null;
			Result = null;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat("L_{0:x4}: ", CILOffset);

			if (Result != null)
				result.AppendFormat("{0} ({1}) := ", Result.ToString(), Result.Type.Name);

			result.Append(OpCode.ToString());
			if (Extra != null)
				result.AppendFormat("[{0}]", Extra);

			if (Operand1 != null)
				result.AppendFormat(" {0}", Operand1.ToString());

			if (Operand2 != null)
				result.AppendFormat(", {0}", Operand2.ToString());

			return result.ToString();
		}
	}
}
