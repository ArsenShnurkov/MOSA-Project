/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using System.Collections;

namespace Mosa.Compiler.Analysis.RegisterAllocator
{
	class BlockLiveness
	{
		public Instruction ActualBegin { get; set; }

		public BitArray Gen { get; set; }

		public BitArray Kill { get; set; }

		public BitArray KillNot { get; set; }

		public BitArray In { get; set; }

		public BitArray Out { get; set; }
	}
}
