/*
 * (c) 2013 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System;

namespace Mosa.Compiler.MosaTypeSystem
{
	public class MosaParameter : MosaUnit, IEquatable<MosaParameter>, IEquatable<MosaType>
	{
		public MosaParameterAttributes ParameterAttributes { get; private set; }

		public MosaMethod DeclaringMethod { get; private set; }

		public MosaType Type { get; private set; }

		internal MosaParameter()
		{
		}

		public MosaParameter(string name, MosaType type)
		{
			// set base fields
			var m = new Mutator(this);
			m.Name = name;
			// this fields
			this.Type = type;
		}

		internal MosaParameter Clone()
		{
			return (MosaParameter)base.MemberwiseClone();
		}

		public override string ToString()
		{
			return Type + " " + Name;
		}

		public bool Equals(MosaParameter parameter)
		{
			return Type.Equals(parameter.Type) 
				&& ParameterAttributes.Equals(parameter.ParameterAttributes)
				/*&& CustomAttributes.Equals(parameter.CustomAttributes)*/
				;
		}

		public bool Equals(MosaType type)
		{
			return Type.Equals(type);
		}

		public class Mutator : MosaUnit.MutatorBase
		{
			private MosaParameter parameter;

			internal Mutator(MosaParameter parameter)
				: base(parameter)
			{
				this.parameter = parameter;
			}

			public MosaParameterAttributes ParameterAttributes { set { parameter.ParameterAttributes = value; } }

			public MosaMethod DeclaringMethod { set { parameter.DeclaringMethod = value; } }

			public MosaType ParameterType { set { parameter.Type = value; } }

			public override void Dispose()
			{
				if (parameter.Type != null)
				{
					parameter.FullName = string.Concat(parameter.Type.FullName, " ", parameter.DeclaringMethod.FullName, "::", parameter.Name);
					parameter.ShortName = string.Concat(parameter.Name, " : ", parameter.Type.ShortName);
				}
			}
		}
	}
}
