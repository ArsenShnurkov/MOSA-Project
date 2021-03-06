﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.Test.Collection
{
	public static class StringTests
	{
		public static string valueA = "Foo";
		public static string valueB = "Something";

		public static int CheckLength()
		{
			return valueA.Length;
		}

		public static char CheckFirstCharacter()
		{
			return valueA[0];
		}

		public static char CheckLastCharacter()
		{
			return valueA[valueA.Length - 1];
		}

		public static char LastCharacterMustMatch()
		{
			char ch = '\0';

			for (int index = 0; index < valueB.Length; index++)
			{
				ch = valueB[index];
			}

			return ch;
		}
	}
}