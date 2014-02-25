/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

using Mosa.Compiler.MosaTypeSystem;

namespace Mosa.Compiler.Analysis.Values
{
	public class StackValue : Value
	{
		public StackValue(MosaType type)
		{
			base.Type = type;
		}
	}
}
