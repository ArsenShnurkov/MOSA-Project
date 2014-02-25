/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Blocks
{
	public class ScopeBlock : BlockBase
	{
		public BlockType Type { get; private set; }

		public IList<BlockBase> Blocks { get; private set; }

		public MosaType ExceptionType { get; set; }

		public ScopeBlock TryBlock { get; set; }

		public ScopeBlock FilterBlock { get; set; }

		public ScopeBlock HandlerBlock { get; set; }

		public ScopeBlock(BlockType type)
		{
			Type = type;
			Blocks = new BlockCollection(this);
		}

		class BlockCollection : Collection<BlockBase>
		{
			ScopeBlock parent;
			public BlockCollection(ScopeBlock parent)
			{
				this.parent = parent;
			}

			protected override void InsertItem(int index, BlockBase item)
			{
				if (item.Parent != null)
					throw new ArgumentException("Block already has parent.", "item");
				else
					item.Parent = parent;
				base.InsertItem(index, item);
			}

			protected override void ClearItems()
			{
				foreach (var block in Items)
					block.Parent = null;
				base.ClearItems();
			}

			protected override void RemoveItem(int index)
			{
				Items[index].Parent = null;
				base.RemoveItem(index);
			}

			protected override void SetItem(int index, BlockBase item)
			{
				if (Items[index] != null)
					Items[index].Parent = null;

				if (item.Parent != null)
					throw new ArgumentException("Block already has parent.", "item");
				else
					item.Parent = parent;

				base.SetItem(index, item);
			}
		}
	}
}
