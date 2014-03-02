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
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.Analysis.Blocks;

namespace Mosa.Compiler.Analysis.Operands
{
	public class PhiOperand : ValueOperand
	{
		public BasicBlock SourceBlock { get; private set; }

		private PhiOperand() { }

		public static PhiOperand Create(SSAValue value, BasicBlock block)
		{
			return new PhiOperand() { Value = value, SourceBlock = block };
		}
	}
}
