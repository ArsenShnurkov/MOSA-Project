/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 */

using Mosa.Platform.Internal.x86;
using Mosa.Kernel.x86;
using Mosa.DeviceDrivers.ISA;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public static class CMOSExtensions
	{
		/// <summary>
		/// Dump multiboot info.
		/// </summary>
		public static void Dump(this CMOS cmos, ConsoleSession console, uint row, uint col)
		{
			console.Row = row;
			console.Column = col;
			console.Color = 0x0A;
			console.Write(@"CMOS:");
			console.WriteLine();

			for (byte i = 0; i < 19; i++)
				{
					console.Column = col;
					console.Color = 0x0F;
					console.Write(i, 16, 2);
					console.Write(':');
					console.Write(' ');
					console.Write(cmos.Get(i), 16, 2);
					console.Write(' ');
					console.Color = 0x07;
					console.Write(cmos.Get(i), 10, 3);
					console.WriteLine();
				}
		}

	}
}
