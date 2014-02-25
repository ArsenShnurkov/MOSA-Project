/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Operands
{
	public class StaticFieldOperand : Operand
	{
		public MosaField Field { get; private set; }

		public override MosaType Type { get { return Field.FieldType; } }

		private StaticFieldOperand() { }

		public static StaticFieldOperand Create(MosaField field)
		{
			return new StaticFieldOperand() { Field = field };
		}

		public override string ToString()
		{
			return string.Format("[{0}]", Field);
		}
	}
}
