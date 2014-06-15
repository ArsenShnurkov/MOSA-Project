/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Kernel.x86.Smbios;
using Mosa.Platform.Internal.x86;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public static class Kernel
	{
		public static void Setup()
		{
			IDT.SetInterruptHandler(null);
			SSE.Setup();
			Multiboot.Setup();

			Native.Cli();
			try
			{
				PIC.Setup();
				GDT.Setup();
				IDT.Setup();
				PageFrameAllocator.Setup();
				VirtualPageAllocator.Setup();
				PageTable.Setup();
			}
			finally
			{
				Native.Sti ();
			}

			ProcessManager.Setup();
			TaskManager.Setup();
			SmbiosManager.Setup();
			ConsoleManager.Setup();

			//Serial.Setup();
		}
	}
}