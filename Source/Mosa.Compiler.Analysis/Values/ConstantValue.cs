/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Values
{
	public class ConstantValue : Value
	{
		public object Constant { get; private set; }

		private ConstantValue() { }

		public override Operand ToOperand()
		{
			return ConstantOperand.Create(Type, Constant);
		}

		public static ConstantValue Create(TypeSystem typeSystem, object value, MosaTypeCode typeCode)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.GetTypeFromTypeCode(typeCode)
			};
		}

		public static ConstantValue CreateI4(TypeSystem typeSystem, int value)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.BuiltIn.I4
			};
		}

		public static ConstantValue CreateI8(TypeSystem typeSystem, long value)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.BuiltIn.I8
			};
		}

		public static ConstantValue CreateString(TypeSystem typeSystem, string value)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.BuiltIn.String
			};
		}

		public static ConstantValue CreateR4(TypeSystem typeSystem, float value)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.BuiltIn.R4
			};
		}

		public static ConstantValue CreateR8(TypeSystem typeSystem, double value)
		{
			return new ConstantValue()
			{
				Constant = value,
				Type = typeSystem.BuiltIn.R8
			};
		}

		public static ConstantValue CreateNull(TypeSystem typeSystem)
		{
			return new ConstantValue()
			{
				Constant = null,
				Type = typeSystem.BuiltIn.Object
			};
		}

		public override string ToString()
		{
			if (Constant == null)
				return "null";
			else if (Constant is int)
				return string.Format("{0:x8}h", Constant);
			else if (Constant is long)
				return string.Format("{0:x16}h", Constant);
			else if (Constant is string)
				return string.Format("\"{0}\"", Constant.ToString().Replace("\"", "\\\"").Replace("\\", "\\\\"));
			else if (Constant is float)
				return string.Format("{0}", Constant);
			else if (Constant is double)
				return string.Format("{0}", Constant);
			else
				return string.Format("\"{0}\"", Type.Name.ToUpper(), Constant.ToString().Replace("\"", "\\\"").Replace("\\", "\\\\"));
		}
	}
}
