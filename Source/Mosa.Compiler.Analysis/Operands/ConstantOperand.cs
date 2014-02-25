/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Operands
{
	public class ConstantOperand : Operand
	{
		public object Constant { get; private set; }

		private ConstantOperand() { }

		MosaType type;
		public override MosaType Type { get { return type; } }

		public ConstantOperand ChangeType(MosaType type)
		{
			return new ConstantOperand()
			{
				Constant = this.Constant,
				type = type
			};
		}

		public static ConstantOperand Create(MosaType type, object value)
		{
			return new ConstantOperand()
			{
				Constant = value,
				type = type
			};
		}

		public override string ToString()
		{
			if (Constant == null)
				return "null";
			else if (Constant is int)
				return string.Format("{0:x8}h ({1})", Constant, Type.Name);
			else if (Constant is long)
				return string.Format("{0:x16}h ({1})", Constant, Type.Name);
			else if (Constant is string)
				return string.Format("\"{0}\" ({1})", Constant.ToString().Replace("\"", "\\\"").Replace("\\", "\\\\"), Type.Name);
			else if (Constant is float)
				return string.Format("{0} ({1})", Constant, Type.Name);
			else if (Constant is double)
				return string.Format("{0} ({1})", Constant, Type.Name);
			else
				return string.Format("\"{0}\" ({1})", Type.Name.ToUpper(), Constant.ToString().Replace("\"", "\\\"").Replace("\\", "\\\\"), Type.Name);
		}
	}
}
