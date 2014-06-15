/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Kernel.x86.Smbios;

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
			PIC.Setup();
			GDT.Setup();
			IDT.Setup();
			PageFrameAllocator.Setup();
			uint pageCount = (uint)(512ul * 1024ul * 1024ul / PageFrameAllocator.PageSize); // pages for 512M RAM
			PageTable.Setup(pageCount);
			VirtualPageAllocator.Setup();
			ProcessManager.Setup();
			TaskManager.Setup();
			SmbiosManager.Setup();
			ConsoleManager.Setup();

			//Serial.Setup();
		}
	}
}