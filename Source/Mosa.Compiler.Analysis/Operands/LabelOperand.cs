/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Operands
{
	public class LabelOperand : Operand
	{
		public BasicBlock Target { get; private set; }

		public override MosaType Type { get { return null; } }

		private LabelOperand() { }

		public static LabelOperand Create(BasicBlock target)
		{
			return new LabelOperand() { Target = target };
		}

		public override string ToString()
		{
			return "Block_" + Target.ID;
		}
	}
}
