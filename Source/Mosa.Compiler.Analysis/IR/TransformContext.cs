/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using Mosa.Compiler.Analysis.Blocks;
using Mosa.Compiler.Analysis.CIL;
using Mosa.Compiler.Analysis.Dominance;
using Mosa.Compiler.Analysis.Values;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.IR
{
	internal class TransformContext
	{
		public TypeSystem TypeSystem { get; private set; }

		public MethodBody Body { get; private set; }

		public BasicBlock Block { get; private set; }

		public BlockInfo CurrentBlockInfo { get; private set; }

		public InstrPointer IRPointer { get; private set; }

		public MosaInstruction Instruction { get; internal set; }

		public MosaInstruction Prefix { get; internal set; }

		public EvaluationStack EvaluationStack { get; internal set; }

		public IDictionary<int, BasicBlock> BlockHeaders { get; private set; }

		public IDictionary<int, BlockInfo> BlockInfos { get; private set; }

		public IDominanceProvider Dominance { get; private set; }

		List<SSAValue>[] localVers;

		public TransformContext(MethodBody body, IList<BasicBlock> blocks)
		{
			Body = body;
			TypeSystem = body.Method.TypeSystem;

			BlockHeaders = new Dictionary<int, BasicBlock>();
			foreach (var block in blocks)
			{
				if (!block.IsTechnicalBlock)
					BlockHeaders.Add(block.GetCILOffset(), block);
			}
			BlockInfos = new Dictionary<int, BlockInfo>();

			Dominance = new SimpleFastDominance(body.EntryBlock);

			localVers = new List<SSAValue>[body.Locals.Count];
			for (int i = 0; i < body.Locals.Count; i++)
				localVers[i] = new List<SSAValue>() { new SSAValue(body.Locals[i], 0) };

			IRPointer = new InstrPointer();
		}

		internal void SetBlock(BasicBlock block)
		{
			Block = block;
			IRPointer.SetBlock(block).Last();
			CurrentBlockInfo = BlockInfos[block.Sequence];
		}

		public Value GetLocal(int index)
		{
			return localVers[index][CurrentBlockInfo.SSAVersion[index]];
		}

		public Value GetParameter(int index)
		{
			return Body.Parameters[index];
		}

		public Value CreateVReg(MosaType type)
		{
			return Body.CreateVReg(type);
		}

		public void SetLocal(int index)
		{
			var versions = localVers[index];
			var newVer = new SSAValue(Body.Locals[index], versions.Count);
			versions.Add(newVer);
			CurrentBlockInfo.SSAVersion[index] = newVer.Version;
		}

		public SSAValue GetLocalVersion(int index, int ver)
		{
			return localVers[index][ver];
		}

		public SSAValue GetLocalNewVersion(int index)
		{
			var versions = localVers[index];
			var newVer = new SSAValue(Body.Locals[index], versions.Count);
			versions.Add(newVer);
			return newVer;
		}
	}
}
