/*
 * (c) 2014 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Ki (kiootic) <kiootic@gmail.com>
 */

namespace Mosa.Compiler.Analysis.IR
{
	public static class ConditionCodeExtension
	{
		public static ConditionCode Reverse(this ConditionCode condCode)
		{
			switch (condCode)
			{
				case ConditionCode.Equal: return ConditionCode.NotEqual;
				case ConditionCode.NotEqual: return ConditionCode.Equal;
				case ConditionCode.GreaterOrEqual: return ConditionCode.LessThan;
				case ConditionCode.GreaterThan: return ConditionCode.LessOrEqual;
				case ConditionCode.LessOrEqual: return ConditionCode.GreaterThan;
				case ConditionCode.LessThan: return ConditionCode.GreaterOrEqual;
				case ConditionCode.UnsignedGreaterOrEqual: return ConditionCode.UnsignedLessThan;
				case ConditionCode.UnsignedGreaterThan: return ConditionCode.UnsignedLessOrEqual;
				case ConditionCode.UnsignedLessOrEqual: return ConditionCode.UnsignedGreaterThan;
				case ConditionCode.UnsignedLessThan: return ConditionCode.UnsignedGreaterOrEqual;
			}
			return condCode;
		}

		public static ConditionCode ToUnsigned(this ConditionCode condCode)
		{
			switch (condCode)
			{
				case ConditionCode.LessThan: return ConditionCode.UnsignedLessThan;
				case ConditionCode.LessOrEqual: return ConditionCode.UnsignedLessOrEqual;
				case ConditionCode.GreaterThan: return ConditionCode.UnsignedGreaterThan;
				case ConditionCode.GreaterOrEqual: return ConditionCode.UnsignedGreaterOrEqual;
			}
			return condCode;
		}

		public static ConditionCode ToSigned(this ConditionCode condCode)
		{
			switch (condCode)
			{
				case ConditionCode.UnsignedLessThan: return ConditionCode.LessThan;
				case ConditionCode.UnsignedLessOrEqual: return ConditionCode.LessOrEqual;
				case ConditionCode.UnsignedGreaterThan: return ConditionCode.GreaterThan;
				case ConditionCode.UnsignedGreaterOrEqual: return ConditionCode.GreaterOrEqual;
			}
			return condCode;
		}
	}
}
