/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.DeviceSystem;

namespace Mosa.CoolWorld.x86
{
	/// <summary>
	/// CMOS Device Driver
	/// </summary>
	[ISADeviceDriver(AutoLoad = true, BasePort = 0x0070, PortRange = 2, Platforms = PlatformArchitecture.X86)]
	public class CMOS : Mosa.DeviceDrivers.ISA.CMOS
	{
		/// <summary>
		///
		/// </summary>
		public CMOS()
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
			dataPort = base.hardwareResources.GetIOPort(0, 1);

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
		/// Gets the second.
		/// </summary>
		/// <value>The second.</value>
		public byte Second { get { return Read(0); } }

		/// <summary>
		/// Gets the minute.
		/// </summary>
		/// <value>The minute.</value>
		public byte Minute { get { return Read(2); } }

		/// <summary>
		/// Gets the hour.
		/// </summary>
		/// <value>The hour.</value>
		public byte Hour { get { return Read(4); } }

		/// <summary>
		/// Gets the year.
		/// </summary>
		/// <value>The year.</value>
		public byte Year { get { return Read(9); } }

		/// <summary>
		/// Gets the month.
		/// </summary>
		/// <value>The month.</value>
		public byte Month { get { return Read(8); } }

		/// <summary>
		/// Gets the day.
		/// </summary>
		/// <value>The day.</value>
		public byte Day { get { return Read(7); } }
	}
}