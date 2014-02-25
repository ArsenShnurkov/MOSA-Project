/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis
{
	public abstract class Value
	{
		public MosaType Type { get; set; }

		public Instruction Definition { get; internal set; }
		public IList<Instruction> Usages { get; private set; }

		public Value()
		{
			Usages = new List<Instruction>();
		}

		ValueOperand operand;
		public virtual Operand ToOperand()
		{
			return operand ?? (operand = ValueOperand.Create(this));
		}
	}
}
