/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections.Generic;

namespace Mosa.Compiler.Analysis.RegisterAllocator
{
	public class LiveInterval
	{
		public Value Value { get; private set; }

		public IList<Interval> Intervals { get; private set; }

		public LiveInterval(Value val)
		{
			this.Value = val;
			Intervals = new List<Interval>();
		}

		public bool Contains(uint seqId)
		{
			foreach (var interval in Intervals)
			{
				if (interval.Contains(seqId))
					return true;
			}
			return false;
		}

		public void AddInterval(uint begin, uint end)
		{
			Intervals.Add(new Interval(begin, end));
		}
	}
}
