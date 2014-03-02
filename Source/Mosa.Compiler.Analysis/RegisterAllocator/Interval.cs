/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

namespace Mosa.Compiler.Analysis.RegisterAllocator
{
	public struct Interval
	{
		public Interval(uint begin, uint end)
		{
			this.begin = begin;
			this.end = end;
		}

		private uint begin;
		private uint end;

		public uint Begin { get { return begin; } }
		public uint End { get { return end; } }

		public bool Contains(uint seqId)
		{
			return seqId >= begin && seqId <= end;
		}
	}
}
