/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Kernel.x86;
using Mosa.Kernel.x86.Smbios;
using Mosa.Platform.Internal.x86;
using Mosa.DeviceDrivers.ISA;
using Mosa.DeviceSystem;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public class Kernel_x86
	{
		public static IHardwareAbstraction hal = null;
		public static CMOS CMOS = null;
		public static PIC PIC = null;
		public static ProcessManager ProcessManager = null;

		public static void Setup()
		{
			IDT.SetInterruptHandler(null);
			SSE.Setup();
			GDT.Setup();
			IDT.Setup();
			PageFrameAllocator.Setup();
			PageTable.Setup();
			VirtualPageAllocator.Setup();

			SmbiosManager.Setup();

			CMOS = new CMOS(hal);
			Multiboot.Setup();
			PIC = new PIC(hal);
			PIC.Setup();
			DebugClient.Setup(Serial.COM1);

			ProcessManager.Setup();
			//Runtime.Metadata_InitializeLookup();
			TaskManager.Setup();
			ConsoleManager.Setup();
		}
	}
}
