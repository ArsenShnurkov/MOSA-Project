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
	public class LocalValue : Value
	{
		public LocalValue(MosaLocal local, MosaType type)
		{
			this.Type = type;
			this.LocalVariable = local;
		}

		public MosaLocal LocalVariable { get; private set; }

		public override string ToString()
		{
			return LocalVariable.Name;
		}
	}
}
