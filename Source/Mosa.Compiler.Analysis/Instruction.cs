/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;
using System.Text;
using Mosa.Compiler.Analysis.IR;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis
{
	public class Instruction
	{
		internal Instruction()
		{
		}

		public uint OriginOffset { get; set; }

		OpCode opCode;
		public OpCode OpCode
		{
			get { return opCode; }
			set
			{
				if (opCode != value)
				{
					opCode = value;

					if (operands == null || operands.Length < opCode.OperandCount)
						Array.Resize(ref operands, opCode.OperandCount);

					if (results == null || results.Length < opCode.ResultCount)
					Array.Resize(ref results, opCode.ResultCount);
				}
			}
		}

		public struct OperandCollection
		{
			public Instruction instr;
			public Operand this[int index]
			{
				get { return instr.operands[index]; }
				set
				{
					instr.SetUsage(instr.operands[index], value);
					instr.operands[index] = value;
				}
			}

			public int Count { get { return instr.operands.Length; } }
		}

		public struct ResultCollection
		{
			public Instruction instr;
			public Value this[int index]
			{
				get { return instr.results[index]; }
				set
				{
					instr.SetDefinition(instr.results[index], value);
					instr.results[index] = value;
				}
			}

			public int Count { get { return instr.results.Length; } }
		}

		Operand[] operands;
		public OperandCollection Operands { get; private set; }

		public Operand Operand1
		{
			get { return operands[0]; }
			set
			{
				SetUsage(operands[0], value);
				operands[0] = value;
			}
		}

		public Operand Operand2
		{
			get { return operands[1]; }
			set
			{
				SetUsage(operands[1], value);
				operands[1] = value;
			}
		}

		public void SetOperands(Operand[] operands)
		{
			this.operands = operands;
		}

		Value[] results;
		public ResultCollection Results { get; private set; }

		public Value Result
		{
			get { return results[0]; }
			set
			{
				SetDefinition(results[0], value);
				results[0] = value;
			}
		}

		void SetUsage(Operand originalVal, Operand newVal)
		{
			// Remove
			if (originalVal is ValueOperand)
			{
				((ValueOperand)originalVal).Value.Usages.Remove(this);
			}

			// Add
			if (newVal is ValueOperand)
			{
				((ValueOperand)newVal).Value.Usages.Add(this);
			}
		}

		void SetDefinition(Value originalVal, Value newVal)
		{
			if (newVal != null)
			{
				if (newVal.Definition != null)
				{
					throw new CompilerException("Attempted to violate SSA property.");
				}
				newVal.Definition = this;
			}
			if (originalVal != null)
				originalVal.Definition = null;
		}

		public object Extra { get; set; }

		public void Clear()
		{
			OpCode = IROpCodes.Nop;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat("L_{0:x4}: ", OriginOffset);

			if (results.Length > 1)
			{
				result.Append("{");
				for (int i = 0; i < results.Length; i++)
				{
					if (i == 0)
						result.Append(", ");
					result.AppendFormat("{0} ({1})", results[i].ToString(), results[i].Type.Name);
				}
				result.Append("}");
			}
			else if (results.Length > 0)
			{
				result.AppendFormat("{0} ({1}) := ", results[0].ToString(), results[0].Type.Name);
			}

			result.Append(OpCode.ToString());
			if (Extra != null)
				result.AppendFormat("[{0}]", Extra);

			for (int i = 0; i < operands.Length; i++)
			{
				if (i == 0)
					result.Append(" ");
				else
					result.Append(", ");
				result.Append(operands[i].ToString());
			}

			return result.ToString();
		}
	}
}
