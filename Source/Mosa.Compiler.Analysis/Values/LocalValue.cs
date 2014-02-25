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
	public class LocalValue : StackValue
	{
		public LocalValue(MosaLocal local, MosaType type, bool regOk)
			: base(type)
		{
			this.LocalVariable = local;
			this.CanBeInRegister = regOk;
		}

		public MosaLocal LocalVariable { get; private set; }

		public bool CanBeInRegister { get; set; }

		public override string ToString()
		{
			return LocalVariable.Name;
		}
	}
}
