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
			Operands = new OperandCollection() { instr = this };
			Results = new ResultCollection() { instr = this };
		}

		public uint OriginOffset { get; set; }

		public uint Sequence { get; set; }

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

					operandCount = opCode.OperandCount;
					for (int i = operandCount; i < operands.Length; i++)
							SetUsage(operands[i], null);

					if (results == null || results.Length < opCode.ResultCount)
						Array.Resize(ref results, opCode.ResultCount);

					resultCount = opCode.ResultCount;
					for (int i = resultCount; i < results.Length; i++)
							SetDefinition(results[i], null);
				}
			}
		}

		public class OperandCollection
		{
			internal Instruction instr;
			public Operand this[int index]
			{
				get { return instr.operands[index]; }
				set
				{
					instr.SetUsage(instr.operands[index], value);
					instr.operands[index] = value;
				}
			}

			public int Count { get { return instr.operandCount; } }
		}

		public class ResultCollection
		{
			internal Instruction instr;
			public Value this[int index]
			{
				get { return instr.results[index]; }
				set
				{
					instr.SetDefinition(instr.results[index], value);
					instr.results[index] = value;
				}
			}

			public int Count { get { return instr.resultCount; } }
		}

		Operand[] operands;
		int operandCount;
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
			foreach (var operand in this.operands)
				SetUsage(operand, null);

			this.operands = (Operand[])operands.Clone();
			operandCount = operands.Length;

			foreach (var operand in this.operands)
				SetUsage(null, operand);
		}

		Value[] results;
		int resultCount;
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
			if (originalVal == newVal) 
				return;

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
			if (originalVal == newVal)
				return;

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

			if (resultCount > 1)
			{
				result.Append("{");
				for (int i = 0; i < resultCount; i++)
				{
					if (i == 0)
						result.Append(", ");
					result.AppendFormat("{0} ({1})", results[i] == null ? "<<NULL>>" : results[i].ToString(), results[i].Type.Name);
				}
				result.Append("}");
			}
			else if (resultCount > 0)
			{
				result.AppendFormat("{0} ({1}) := ", results[0] == null ? "<<NULL>>" : results[0].ToString(), results[0].Type.Name);
			}

			result.Append(OpCode.ToString());
			if (Extra != null)
				result.AppendFormat("[{0}]", Extra);

			for (int i = 0; i < operandCount; i++)
			{
				if (i == 0)
					result.Append(" ");
				else
					result.Append(", ");
				result.Append(operands[i] == null ? "<<NULL>>" : operands[i].ToString());
			}

			return result.ToString();
		}
	}
}
