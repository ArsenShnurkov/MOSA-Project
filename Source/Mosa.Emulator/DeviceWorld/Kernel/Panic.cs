/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Platform.Internal.x86;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public static class Panic
	{
		/// <summary>
		/// Nows this instance.
		/// </summary>
		public static void Now()
		{
			Now(0);
		}

		/// <summary>
		/// Nows the specified error code.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		public static void Now(uint errorCode)
		{
			Screen.Column = 0;
			Screen.Row = 0;
			Screen.Color = 0x0C;

			// Writes PANIC! message
			// (this comment was added to allow finding this place with full text search)
			Screen.Write('P');
			Screen.Write('A');
			Screen.Write('N');
			Screen.Write('I');
			Screen.Write('C');
			Screen.Write('!');
			Screen.Write(' ');
			// To find all error codes, please do full text search for "Panic.Now"
			Screen.Write(errorCode, 10, 20);

			while (true)
				Native.Hlt();
		}

		public static void Write(uint x, uint y, string letters)
		{
			byte color = 0x0C;
			uint v = x;
			for (int i =0 ; i < letters.Length; ++i)
			{
				Screen.RawWrite(y, v++, letters[i], color);
			}
		}

		public static void Number(uint x, uint y, uint n, byte digitBase, int digitCount)
		{
			Screen.Color = 0x0E;
			Screen.Column = x;
			Screen.Row = y;
			Screen.Write (n, digitBase, digitCount);
		}
	}
}