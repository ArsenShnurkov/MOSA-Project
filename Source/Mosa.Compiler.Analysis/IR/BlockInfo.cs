/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.CIL;
using Mosa.Compiler.Analysis.Operands;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.Common;

namespace Mosa.Compiler.Analysis.IR
{
	internal class BlockInfo
	{
		public BlockInfo(BasicBlock block)
		{
			Block = block;
			Status = StatusAwait;
		}

		public const int StatusAwait = 1;
		public const int StatusQueued = 2;
		public const int StatusProcessed = 3;

		public BasicBlock Block;
		public EvaluationStack EvalutionStack;

		// Stack phi
		public VRegValue[] StackValues;
		public Tuple<Instruction, SSAValue[]>[] StackPhis;

		// Local phi
		public int[] SSAVersion;
		public Tuple<Instruction, SSAValue[]>[] LocalPhis;

		public int Status;
	}

}
