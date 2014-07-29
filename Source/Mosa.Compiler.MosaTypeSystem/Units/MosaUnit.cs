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
using System.Text;

namespace Mosa.Compiler.MosaTypeSystem
{
	public abstract class MosaUnit
	{
		public object UnderlyingObject { get; private set; }

		public uint ID { get; internal set; }

		public TypeSystem TypeSystem { get; internal set; }

		public string Name { get; private set; }

		public string FullName { get; internal set; }

		public string ShortName { get; internal set; }

		public bool IsLinkerGenerated { get; private set; }

		private MosaCustomAttributeList customAttributes;

		public IList<MosaCustomAttribute> CustomAttributes { get; private set; }

		internal MosaUnit()
		{
			CustomAttributes = (customAttributes = new MosaCustomAttributeList()).AsReadOnly();
			Name = "";
		}

		public T GetUnderlyingObject<T>()
		{
			return (T)UnderlyingObject;
		}

		public override string ToString()
		{
			return FullName;
		}

		public MosaCustomAttribute FindCustomAttribute(string fullName)
		{
			foreach (var attr in customAttributes)
			{
				if (attr.Constructor.DeclaringType.FullName == fullName)
					return attr;
			}
			return null;
		}

		public abstract class MutatorBase : IDisposable
		{
			private MosaUnit unit;

			internal MutatorBase(MosaUnit unit)
			{
				this.unit = unit;
			}

			public object UnderlyingObject { set { unit.UnderlyingObject = value; } }

			public string Name { set { unit.Name = value; } }

			public bool IsLinkerGenerated { set { unit.IsLinkerGenerated = value; } }

			public IList<MosaCustomAttribute> CustomAttributes { get { return unit.customAttributes; } }

			public abstract void Dispose();
		}
	}

	public class ListFormatProvider : IFormatProvider, ICustomFormatter
	{
#region IFormatProvider implementation

		public object GetFormat(Type formatType)
		{
			if (formatType.Equals(typeof(ICustomFormatter)))
			{
				return this;
			}
			throw new NotSupportedException();
		}

#endregion

#region ICustomFormatter implementation

		public string Format(string format, object o, IFormatProvider formatProvider)
		{
			var res = new StringBuilder();
			res.AppendLine(string.Format("{0} ->", name));
			if (o is IEnumerable<object>)
			{
				var list = (IEnumerable<object>)o;
				foreach (var m in list)
				{
					if (format == null)
					{
						format = "<{0}>";
					}
					res.AppendLine(string.Format(format, m.ToString()));
				}
			}
			return res.ToString();
		}

#endregion

		protected string name;

		public ListFormatProvider(string name)
		{
			this.name = name;
		}
	}
}
