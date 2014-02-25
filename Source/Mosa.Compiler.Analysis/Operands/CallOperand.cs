/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Text;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.IR.Operands
{
	public class CallOperand : Operand
	{
		public CallOperand(MosaType returnType, Value[] arguments)
		{
			this.returnType = returnType;
			Arguments = arguments;
		}

		public Value[] Arguments { get; private set; }

		MosaType returnType;
		public override MosaType Type { get { return returnType; } }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.Append("{");
			for (int i = 0; i < Arguments.Length; i++)
			{
				if (i != 0)
					result.Append(", ");
				result.Append(Arguments[i].ToString());
			}
			return result.AppendFormat("}} ({0})", returnType.Name).ToString();
		}
	}
}
