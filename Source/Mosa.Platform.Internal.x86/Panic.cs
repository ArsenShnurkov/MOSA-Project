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

		public static void Message(uint x, uint y, string msg)
		{
			Screen.Color = 0x0C;
			Screen.Row = y;
			for (uint i = 0; i < msg.Length; ++i)
			{
				Screen.Column = x + i;
				Screen.Write (msg[(int)i]);
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