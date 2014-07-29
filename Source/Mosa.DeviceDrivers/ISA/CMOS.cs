/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.DeviceSystem;

namespace Mosa.DeviceDrivers.ISA
{
	/// <summary>
	/// CMOS Device Driver
	/// </summary>
	[ISADeviceDriver(AutoLoad = true, BasePort = 0x0070, PortRange = 2, Platforms = PlatformArchitecture.X86)]
	public class CMOS : HardwareDevice, IDevice, IHardwareDevice
	{
		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort commandPort;

		/// <summary>
		///
		/// </summary>
		protected IReadWriteIOPort dataPort;

		/// <summary>
		///
		/// </summary>
		protected SpinLock spinLock;

		/// <summary>
		///
		/// </summary>
		public CMOS(IHardwareAbstraction board) : base(board)
		{
		}

		/// <summary>
		/// Setups this hardware device driver
		/// </summary>
		/// <returns></returns>
		public override bool Setup(IHardwareResources hardwareResources)
		{
			this.hardwareResources = hardwareResources;
			base.name = "CMOS";

			commandPort = base.hardwareResources.GetIOPort(0, 0);
			dataPort = base.hardwareResources.GetIOPort(0, 4);
			//dataPort = base.hardwareResources.GetIOPort(0, 1);

			return true;
		}

		/// <summary>
		/// Starts this hardware device.
		/// </summary>
		/// <returns></returns>
		public override DeviceDriverStartStatus Start()
		{
			base.deviceStatus = DeviceStatus.Online;
			return DeviceDriverStartStatus.Started;
		}

		/// <summary>
		/// Called when an interrupt is received.
		/// </summary>
		/// <returns></returns>
		public override bool OnInterrupt()
		{
			return true;
		}

		/// <summary>
		/// Reads the specified address.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public byte Read(byte address)
		{
			spinLock.Enter();
			try
			{
				commandPort.Write8(address);
				byte b = dataPort.Read8();
				return b;
			}
			finally
			{
				spinLock.Exit();
			}
		}

		/// <summary>
		/// Gets the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public byte Get(byte index)
		{
			base.system.DisableAllInterrupts();
			try
			{
				base.system.PortOut8(0x70, index);
				base.system.WaitWithNops(3);
				byte result = base.system.PortIn8(0x71);
				return result;
			}
			finally
			{
				base.system.EnableAllInterrupts();
			}
		}

		/// <summary>
		/// Sets the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public void Set(byte index, byte value)
		{
			base.system.DisableAllInterrupts();
			try
			{
				base.system.PortOut8(0x70, index);
				base.system.WaitWithNops(3);
				base.system.PortOut8(0x71, value);
			}
			finally
			{
				base.system.EnableAllInterrupts();
			}
		}

		/// <summary>
		/// Delays the io bus.
		/// </summary>
		private void Delay()
		{
			base.system.PortIn8(0x80);
			base.system.PortOut8(0x80, 0);
		}

		/// <summary>
		/// Gets the second.
		/// </summary>
		/// <value>The second.</value>
		public byte Second { get { return Get(0); } }

		/// <summary>
		/// Gets the minute.
		/// </summary>
		/// <value>The minute.</value>
		public byte Minute { get { return Get(2); } }

		/// <summary>
		/// Gets the hour.
		/// </summary>
		/// <value>The hour.</value>
		public byte Hour { get { return Get(4); } }

		/// <summary>
		/// Gets the year.
		/// </summary>
		/// <value>The year.</value>
		public byte Year { get { return Get(9); } }

		/// <summary>
		/// Gets the month.
		/// </summary>
		/// <value>The month.</value>
		public byte Month { get { return Get(8); } }

		/// <summary>
		/// Gets the day.
		/// </summary>
		/// <value>The day.</value>
		public byte Day { get { return Get(7); } }

		/// <summary>
		/// Gets the BCD.
		/// </summary>
		/// <value>The BCD.</value>
		public bool BCD { get { return (Get(0x0B) & 0x04) == 0x00; } }
	}
}
