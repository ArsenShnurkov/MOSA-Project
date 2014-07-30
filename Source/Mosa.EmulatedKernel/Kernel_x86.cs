using System;
using Mosa.EmulatedKernel;
using Mosa.EmulatedDevices.Emulated;

namespace Mosa.Kernel.x86
{
	public class Kernel_x86
	{
		public static HardwareAbstraction_Emulation hal = null;
		public static CMOS CMOS = null;
//		public static PIC PIC = null;
//		public static ProcessManager ProcessManager = null;

		public static void Setup()
		{
/*
			IDT.SetInterruptHandler(null);
			SSE.Setup();
			GDT.Setup();
			IDT.Setup();
			PageFrameAllocator.Setup();
			PageTable.Setup();
			VirtualPageAllocator.Setup();

			Runtime.Metadata_InitializeLookup();

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
*/
		}
	}
}

