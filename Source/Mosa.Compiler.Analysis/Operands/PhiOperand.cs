/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Text;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Operands
{
	public class PhiOperand : Operand
	{
		public PhiOperand(Value origin, SSAValue[] versions)
		{
			OriginValue = origin;
			Versions = versions;
		}

		public Value OriginValue { get; private set; }

		public SSAValue[] Versions { get; private set; }

		public override MosaType Type { get { return OriginValue.Type; } }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.Append("phi {");
			for (int i = 0; i < Versions.Length; i++)
			{
				if (i != 0)
					result.Append(", ");
				result.Append(Versions[i].ToString());
			}
			return result.AppendFormat("}} ({0})", OriginValue.Type.Name).ToString();
		}
	}
}
