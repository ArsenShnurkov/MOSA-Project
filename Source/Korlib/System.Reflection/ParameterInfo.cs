/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Stefan Andres Charsley (charsleysa) <charsleysa@gmail.com>
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection
{
	[Serializable]
	public class ParameterInfo : ICustomAttributeProvider
	{
		object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no custom attributes of that type.
		/// </summary>
		/// <param name="attributeType">The type of the custom attributes.</param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
		/// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
		object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Indicates whether one or more instance of attributeType is defined on this member.
		/// </summary>
		/// <param name="attributeType">The type of the custom attributes.</param>
		/// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
		/// <returns><c>true</c> if the attributeType is defined on this member; <c>false</c> otherwise.</returns>
		bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException ();
		}

	}
}