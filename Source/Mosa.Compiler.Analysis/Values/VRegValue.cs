/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis
{
	public class VRegValue : Value
	{
		public VRegValue(MosaType type, int index)
		{
			Type = type;
			Index = index;
		}
		public int Index { get; private set; }

		public override string ToString()
		{
			return "R_" + Index;
		}
	}
}
