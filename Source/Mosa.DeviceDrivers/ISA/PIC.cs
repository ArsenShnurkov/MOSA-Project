/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

// Intel® 82093AA I/O Advanced Programmable Interrupt Controller (I/O APIC) Datasheet
// http://www.intel.com/design/chipsets/datashts/29056601.pdf

// 8259A Programmable Interrupt Controller:
// http://pdos.csail.mit.edu/6.828/2005/readings/hardware/8259A.pdf

using Mosa.DeviceSystem;

namespace Mosa.DeviceDrivers.ISA
{
	/// <summary>
	/// Programmable Interrupt Controller (PIC) Device Driver
	/// </summary>
	[ISADeviceDriver(AutoLoad = true, BasePort = 0x20, PortRange = 2, AltBasePort = 0xA0, AltPortRange = 2, Platforms = PlatformArchitecture.X86AndX64)]
	public class PIC : HardwareDevice, IDevice, IHardwareDevice
	{
		#region Definitions

		/// <summary>
		///
		/// </summary>
		protected const byte IRQBaseSize = 0x08;

		/// <summary>
		///
		/// </summary>
		protected const byte MasterIRQBase = 0x20;

		/// <summary>
		///
		/// </summary>
		protected const byte SlaveIRQBase = MasterIRQBase + IRQBaseSize;

		/// <summary>
		///
		/// </summary>
		protected const byte EOI = 0x20;

		#endregion Definitions

		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort masterCommandPort;

		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort masterDataPort;

		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort slaveCommandPort;

		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort slaveDataPort;

		// Interrupt masks must be tracked via the driver
		/// <summary>
		///
		/// </summary>
		protected byte masterInterruptMask;

		/// <summary>
		///
		/// </summary>
		protected byte slaveInterruptMask;

		/// <summary>
		/// Initializes a new instance of the <see cref="PIC"/> class.
		/// </summary>
		public PIC(IHardwareAbstraction board):base(board)
		{
		}

		/// <summary>
		/// Setups this hardware device driver
		/// </summary>
		/// <param name="hardwareResources"></param>
		/// <returns></returns>
		public override bool Setup(IHardwareResources hardwareResources)
		{
			this.hardwareResources = hardwareResources;
			base.name = "PIC_0x" + base.hardwareResources.GetIOPort(0, 0).Address.ToString("X");

			masterCommandPort = base.hardwareResources.GetIOPort(0, 0);
			masterDataPort = base.hardwareResources.GetIOPort(0, 1);

			slaveCommandPort = base.hardwareResources.GetIOPort(1, 0);
			slaveDataPort = base.hardwareResources.GetIOPort(1, 1);

			return true;
		}

		/// <summary>
		/// Starts this hardware device.
		/// </summary>
		/// <returns></returns>
		public override DeviceDriverStartStatus Start()
		{
			byte masterMask;
			byte slaveMask;

			// Save Masks
			masterMask = masterDataPort.Read8();
			slaveMask = slaveDataPort.Read8();

			// ICW1 - Set Initialize Controller & Expect ICW4
			masterCommandPort.Write8(0x11);

			// ICW2 - interrupt offset
			masterDataPort.Write8(MasterIRQBase);

			// ICW3
			masterDataPort.Write8(0x04);

			// ICW4 - Set 8086 Mode
			masterDataPort.Write8(0x01);

			// ICW1 - Set Initialize Controller & Expect ICW4
			slaveCommandPort.Write8(0x11);

			// ICW2 - interrupt offset
			slaveDataPort.Write8(SlaveIRQBase);

			// ICW3
			slaveDataPort.Write8(0x02);

			// ICW4 - Set 8086 Mode
			slaveDataPort.Write8(0x01);

			// Restore Masks
			masterDataPort.Write8(masterMask);
			slaveDataPort.Write8(slaveMask);

			DisableIRQs();

			base.deviceStatus = DeviceStatus.Online;
			return DeviceDriverStartStatus.Started;
		}

		/// <summary>
		/// Called when an interrupt is received.
		/// </summary>
		/// <returns></returns>
		public override bool OnInterrupt()
		{
			return false;
		}

		/// <summary>
		/// Sends the end of interrupt.
		/// </summary>
		/// <param name="irq">The irq.</param>
		public void SendEndOfInterrupt(byte irq)
		{
			if (irq >= 8)
				slaveCommandPort.Write8(EOI);

			masterCommandPort.Write8(EOI);
		}

		/// <summary>
		/// Disables the IRQs.
		/// </summary>
		public void DisableIRQs()
		{
			masterInterruptMask = 0xFF;
			masterDataPort.Write8(masterInterruptMask);

			slaveInterruptMask = 0xFF;
			slaveDataPort.Write8(slaveInterruptMask);
		}

		/// <summary>
		/// Enables the master IRQs.
		/// </summary>
		/// <param name="irq">The irq.</param>
		protected void EnableMasterIRQ(byte irq)
		{
			// Mask out all but the last three bits
			irq = (byte)(irq & 0x07);

			// Cannot mask IRQ2
			if (irq == 2)
				return;

			// Clear bit
			masterInterruptMask = (byte)(masterInterruptMask & (~(1 << irq)));
			masterDataPort.Write8(masterInterruptMask);
		}

		/// <summary>
		/// Enables the slave IRQ.
		/// </summary>
		/// <param name="irq">The irq.</param>
		protected void EnableSlaveIRQ(byte irq)
		{
			// Mask out all but the last three bits
			irq = (byte)(irq & 0x07);

			// Clear bit
			slaveInterruptMask = (byte)(slaveInterruptMask & (~(1 << irq)));
			slaveDataPort.Write8(masterInterruptMask);
		}

		/// <summary>
		/// Enables the IRQ.
		/// </summary>
		/// <param name="irq">The irq.</param>
		public void EnableIRQ(byte irq)
		{
			if ((irq >= MasterIRQBase) && (irq < SlaveIRQBase + IRQBaseSize))
			{
				if (irq < SlaveIRQBase)
					EnableMasterIRQ((byte)(irq - MasterIRQBase));
				else
					EnableSlaveIRQ((byte)(irq - SlaveIRQBase));
			}
		}

		/// <summary>
		/// Disables the master IRQ.
		/// </summary>
		/// <param name="irq">The irq.</param>
		protected void DisableMasterIRQ(byte irq)
		{
			// Mask out all but the last three bits
			irq = (byte)(irq & 0x07);

			// Cannot mask IRQ2
			if (irq == 2)
				return;

			// Set bit
			masterInterruptMask = (byte)(masterInterruptMask | (1 << irq));
			masterDataPort.Write8(masterInterruptMask);
		}

		/// <summary>
		/// Disables the slave IRQ.
		/// </summary>
		/// <param name="irq">The irq.</param>
		protected void DisableSlaveIRQ(byte irq)
		{
			// Mask out all but the last three bits
			irq = (byte)(irq & 0x07);

			// Set bit
			slaveInterruptMask = (byte)(slaveInterruptMask | (1 << irq));
			slaveDataPort.Write8(slaveInterruptMask);
		}

		/// <summary>
		/// Disables the IRQ.
		/// </summary>
		/// <param name="irq">The irq.</param>
		public void DisableIRQ(byte irq)
		{
			if ((irq >= MasterIRQBase) && (irq < SlaveIRQBase + IRQBaseSize))
			{
				if (irq < SlaveIRQBase)
					DisableMasterIRQ((byte)(irq - MasterIRQBase));
				else
					DisableSlaveIRQ((byte)(irq - SlaveIRQBase));
			}
		}

		private const byte ICW1_ICW4 = 0x01;
		private const byte ICW1_SingleCascadeMode = 0x02;
		private const byte ICW1_Interval4 = 0x04;
		private const byte ICW1_LevelTriggeredEdgeMode = 0x08;
		private const byte ICW1_Initialization = 0x10;
		private const byte ICW2_MasterOffset = 0x20;
		private const byte ICW2_SlaveOffset = 0x28;
		private const byte ICW4_8086 = 0x01;
		private const byte ICW4_AutoEndOfInterrupt = 0x02;
		private const byte ICW4_BufferedSlaveMode = 0x08;
		private const byte ICW4_BufferedMasterMode = 0x0C;
		private const byte ICW4_SpecialFullyNested = 0x10;

		private const byte PIC1_Command = 0x20;
		private const byte PIC2_Command = 0xA0;
		private const byte PIC1_Data = 0x21;
		private const byte PIC2_Data = 0xA1;

		public void Setup()
		{
			byte masterMask = base.system.PortIn8(PIC1_Data);
			byte slaveMask = base.system.PortIn8(PIC2_Data);

			// ICW1 - Set Initialize Controller & Expect ICW4
			base.system.PortOut8(PIC1_Command, ICW1_Initialization + ICW1_ICW4);

			// ICW2 - interrupt offset
			base.system.PortOut8(PIC1_Data, ICW2_MasterOffset);

			// ICW3
			base.system.PortOut8(PIC1_Data, 4);

			// ICW4 - Set 8086 Mode
			base.system.PortOut8(PIC1_Data, ICW4_8086);

			// OCW1
			base.system.PortOut8(PIC1_Data, masterMask);

			// ICW1 - Set Initialize Controller & Expect ICW4
			base.system.PortOut8(PIC2_Command, ICW1_Initialization + ICW1_ICW4);

			// ICW2 - interrupt offset
			base.system.PortOut8(PIC2_Data, ICW2_SlaveOffset);

			// ICW3
			base.system.PortOut8(PIC2_Data, 2);

			// ICW4 - Set 8086 Mode
			base.system.PortOut8(PIC2_Data, ICW4_8086);

			// OCW1
			base.system.PortOut8(PIC2_Data, slaveMask);
		}

		/// <summary>
		/// Sends the end of interrupt.
		/// </summary>
		/// <param name="irq">The irq.</param>
		public void SendEndOfInterrupt(uint irq)
		{
			if (irq >= 40) // or untranslated IRQ >= 8
			{
				base.system.PortOut8(PIC2_Command, EOI);
			}

			base.system.PortOut8(PIC1_Command, EOI);
		}
	}
}