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
	public class ValueOperand : Operand
	{
		public Value Value { get; protected set; }

		public override MosaType Type { get { return Value.Type; } }

		protected ValueOperand() { }

		internal static ValueOperand Create(Value value)
		{
			return new ValueOperand() { Value = value };
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", Value.ToString(), Type.Name);
		}
	}
}
