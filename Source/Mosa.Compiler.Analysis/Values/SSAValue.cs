/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

namespace Mosa.Compiler.Analysis.Values
{
	public class SSAValue : Value
	{
		public SSAValue(Value origin, int version)
		{
			base.Type = origin.Type;
			OriginValue = origin;
			Version = version;
		}

		public Value OriginValue { get; private set; }

		public int Version { get; private set; }

		public override string ToString()
		{
			return string.Format("{0}<{1}>", OriginValue.ToString(), Version);
		}
	}
}
