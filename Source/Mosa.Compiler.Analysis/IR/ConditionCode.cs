﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

namespace Mosa.Compiler.Analysis.IR
{
	/// <summary>
	/// Specifies condition codes of comparision or conditional branch.
	/// </summary>
	public enum ConditionCode
	{
		/// <summary>
		/// The undefined
		/// </summary>
		Undefined,

		/// <summary>
		/// Equality comparison.
		/// </summary>
		Equal,

		/// <summary>
		/// Not equal comparison.
		/// </summary>
		NotEqual,

		/// <summary>
		/// Greater-than comparison.
		/// </summary>
		GreaterThan,

		/// <summary>
		/// Greater-than or equal comparison.
		/// </summary>
		GreaterOrEqual,

		/// <summary>
		/// Less-than comparison.
		/// </summary>
		LessThan,

		/// <summary>
		/// Less-than or equal comparison.
		/// </summary>
		LessOrEqual,

		/// <summary>
		/// Unsigned greater than comparison.
		/// </summary>
		UnsignedGreaterThan,

		/// <summary>
		/// Unsigned greater than or equal comparison.
		/// </summary>
		UnsignedGreaterOrEqual,

		/// <summary>
		/// Unsigned less than comparison.
		/// </summary>
		UnsignedLessThan,

		/// <summary>
		/// Unsigned less than or equal comparison.
		/// </summary>
		UnsignedLessOrEqual
	}
}