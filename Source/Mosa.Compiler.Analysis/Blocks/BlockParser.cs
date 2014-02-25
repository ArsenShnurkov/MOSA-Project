/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;
using System.Diagnostics;
using Mosa.Compiler.Analysis.CIL;
using Mosa.Compiler.Common;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Blocks
{
	public class BlockParser
	{
		public static MethodBody Parse(MosaMethod method)
		{
			var headers = FindBlockHeaders(method);
			var basicBlocks = BuildBasicBlocks(method, headers);
			IList<ScopeBlock> ehs;
			MethodBody body = BuildScope(method, basicBlocks, out ehs);
			LinkBlocks(body, basicBlocks, ehs);
			return body;
		}

		static IList<int> FindBlockHeaders(MosaMethod method)
		{
			List<int> headers = new List<int>() { 0 };
			foreach (var eh in method.ExceptionBlocks)
			{
				headers.Add(eh.TryOffset);
				headers.Add(eh.TryEnd);
				headers.Add(eh.HandlerOffset);
				headers.Add(eh.HandlerEnd);
				if (eh.FilterOffset.HasValue)
					headers.Add(eh.FilterOffset.Value);
			}
			foreach (var instr in method.Code)
			{
				CILOpCode opCode = CILOpCodes.OpCodes[instr.OpCode];
				switch (opCode.FlowControl)
				{
					case FlowControl.UnconditionalBranch:
					case FlowControl.Leave:
					case FlowControl.ConditionalBranch:
						if (instr.Next.HasValue)
							headers.Add(instr.Next.Value);
						headers.Add((int)instr.Operand);
						break;

					case FlowControl.Switch:
						if (instr.Next.HasValue)
							headers.Add(instr.Next.Value);
						headers.AddRange((int[])instr.Operand);
						break;

					case FlowControl.Return:
					case FlowControl.Handler:
					case FlowControl.Throw:
						if (instr.Next.HasValue)
							headers.Add(instr.Next.Value);
						break;
				}
			}

			return headers;
		}

		static IList<BasicBlock> BuildBasicBlocks(MosaMethod method, IList<int> headers)
		{
			List<BasicBlock> result = new List<BasicBlock>();
			BasicBlock currentBlock = null;
			int currentId = 1;
			foreach (var instr in method.Code)
			{
				if (headers.Contains(instr.Offset))
				{
					if (currentBlock != null)
					{
						Debug.Assert(currentBlock.CILInstructions.Count > 0);
						result.Add(currentBlock);
					}
					currentBlock = new BasicBlock(currentId++);
				}
				currentBlock.CILInstructions.Add(instr);
			}
			if (currentBlock != null)
				result.Add(currentBlock);
			return result;
		}

		static MethodBody BuildScope(MosaMethod method, IList<BasicBlock> blocks, out IList<ScopeBlock> ehs)
		{
			MethodBody root = new MethodBody(method);
			ehs = new List<ScopeBlock>();

			if (method.ExceptionBlocks.Count == 0)
			{
				foreach (var block in blocks)
					root.Blocks.Add(block);
				return root;
			}

			Stack<ScopeBlock> scopeStack = new Stack<ScopeBlock>();
			var ehScopes = new Dictionary<MosaExceptionHandler, Tuple<ScopeBlock, ScopeBlock, ScopeBlock>>();
			foreach (var eh in method.ExceptionBlocks)
			{
				BlockType tryType;
				switch (eh.HandlerType)
				{
					default:
					case ExceptionHandlerType.Exception:
						tryType = BlockType.TryException; break;

					case ExceptionHandlerType.Finally:
						tryType = BlockType.TryFinally; break;

					case ExceptionHandlerType.Filter:
						tryType = BlockType.TryFilter; break;

					case ExceptionHandlerType.Fault:
						tryType = BlockType.TryFault; break;
				}
				ScopeBlock tryBlock = new ScopeBlock(tryType);
				tryBlock.ExceptionType = eh.Type;
				ScopeBlock handlerBlock = new ScopeBlock(BlockType.Handler);
				tryBlock.HandlerBlock = handlerBlock;
				handlerBlock.TryBlock = tryBlock;

				if (eh.FilterOffset.HasValue)
				{
					ScopeBlock filterBlock = new ScopeBlock(BlockType.Filter);
					tryBlock.FilterBlock = filterBlock;
					filterBlock.TryBlock = tryBlock;
					ehScopes[eh] = Tuple.Create(tryBlock, handlerBlock, filterBlock);
				}
				else
					ehScopes[eh] = Tuple.Create(tryBlock, handlerBlock, (ScopeBlock)null);

				ehs.Add(tryBlock);
			}

			scopeStack.Push(root);
			foreach (var block in blocks)
			{
				foreach (var eh in method.ExceptionBlocks)
				{
					var ehScope = ehScopes[eh];

					if (block.BeginOffset == eh.TryEnd)
						scopeStack.Pop();

					if (block.BeginOffset == eh.HandlerEnd)
						scopeStack.Pop();

					if (eh.FilterOffset.HasValue && block.BeginOffset == eh.HandlerOffset)
					{
						// Filter must precede handler immediately
						Debug.Assert(scopeStack.Peek().Type == BlockType.Filter);
						scopeStack.Pop();
					}
				}
				foreach (var eh in method.ExceptionBlocks)
				{
					var ehScope = ehScopes[eh];

					if (block.BeginOffset == eh.TryOffset)
						scopeStack.Push(ehScope.Item1);

					if (block.BeginOffset == eh.HandlerOffset)
						scopeStack.Push(ehScope.Item2);

					if (eh.FilterOffset.HasValue && block.BeginOffset == eh.FilterOffset)
						scopeStack.Push(ehScope.Item3);
				}
				scopeStack.Peek().Blocks.Add(block);
			}
			Debug.Assert(scopeStack.Count == 1);
			return root;
		}

		static void LinkBlocks(MethodBody body, IList<BasicBlock> blocks, IList<ScopeBlock> ehs)
		{
			Dictionary<int, BasicBlock> blockHeaders = new Dictionary<int, BasicBlock>();
			foreach (var block in blocks)
			{
				blockHeaders.Add(block.CILInstructions[0].Offset, block);
			}
			BasicBlock.Link(body.EntryBlock, blocks[0]);

			foreach (var block in blocks)
			{
				var term = block.CILInstructions[block.CILInstructions.Count - 1];

				bool hasBranchTerm = true;
				CILOpCode opCode = CILOpCodes.OpCodes[term.OpCode];
				switch (opCode.FlowControl)
				{
					case FlowControl.UnconditionalBranch:
					case FlowControl.Leave:
						BasicBlock.Link(block, blockHeaders[(int)term.Operand]);
						break;

					case FlowControl.ConditionalBranch:
						BasicBlock.Link(block, blockHeaders[(int)term.Operand]);
						if (term.Next.HasValue)
							BasicBlock.Link(block, blockHeaders[term.Next.Value]);
						break;

					case FlowControl.Switch:
						foreach (var target in (int[])term.Operand)
							BasicBlock.Link(block, blockHeaders[target]);
						if (term.Next.HasValue)
							BasicBlock.Link(block, blockHeaders[term.Next.Value]);
						break;

					case FlowControl.Return:
						BasicBlock.Link(block, body.ReturnBlock);
						break;

					case FlowControl.Throw:
					case FlowControl.Handler:
						BasicBlock.Link(block, body.AbnormalBlock);
						break;

					default:
						BasicBlock.Link(block, blockHeaders[term.Next.Value]);
						hasBranchTerm = false;
						break;
				} 
				if (hasBranchTerm)
				{
					block.CILInstructions.RemoveAt(block.CILInstructions.Count - 1);
					block.TerminatorInstruction = term;
				}
			}

			foreach (var eh in ehs)
			{
				BasicBlock header = eh.GetFirstBlock();
				Debug.Assert(header != null);
				foreach (var source in header.Sources)
				{
					BasicBlock.Link(source, eh.HandlerBlock.GetFirstBlock());
					if (eh.FilterBlock != null)
						BasicBlock.Link(source, eh.FilterBlock.GetFirstBlock());
				}
			}
		}
	}
}
