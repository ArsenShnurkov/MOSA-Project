/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;

namespace Mosa.Compiler.Analysis.Blocks
{
	public static class BlockExtension
	{
		public static BasicBlock GetFirstBlock(this BlockBase block)
		{
			BlockBase result = block;
			while (!(result is BasicBlock))
			{
				ScopeBlock scope = (ScopeBlock)result;
				if (scope.Blocks.Count == 0)
					return null;
				result = scope.Blocks[0];
			}
			return (BasicBlock)result;
		}

		public static IEnumerable<BasicBlock> GetAllBlocks(this ScopeBlock block)
		{
			Stack<ScopeBlock> scopes = new Stack<ScopeBlock>();
			scopes.Push(block);
			while (scopes.Count > 0)
			{
				foreach (var child in scopes.Pop().Blocks)
				{
					if (child is ScopeBlock)
						scopes.Push((ScopeBlock)child);
					else
						yield return (BasicBlock)child;
				}
			}
		}
	}
}
