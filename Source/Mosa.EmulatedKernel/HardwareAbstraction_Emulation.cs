/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.DeviceSystem;
using Mosa.EmulatedDevices;

namespace Mosa.EmulatedKernel
{
	/// <summary>
	///
	/// </summary>
	public class HardwareAbstraction_Emulation : IHardwareAbstraction
	{
		/// <summary>
		/// Requests an IO read/write port interface from the kernel
		/// </summary>
		/// <param name="port">The port number.</param>
		/// <returns></returns>
		IReadWriteIOPort IPortControllerAbsctraction.RequestIOPort(ushort port)
		{
			return IOPortDispatch.RegisterIOPort(port);
		}

		/// <summary>
		/// Requests a block of memory from the kernel
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		IMemory IMemoryControllerAbstraction.RequestPhysicalMemory(uint address, uint size)
		{
			return MemoryDispatch.RegisterMemory(address, size);
		}

		/// <summary>
		/// Disables all interrupts.
		/// </summary>
		void IHardwareAbstraction.DisableAllInterrupts()
		{
		}

		/// <summary>
		/// Enables all interrupts.
		/// </summary>
		void IHardwareAbstraction.EnableAllInterrupts()
		{
		}

		void IHardwareAbstraction.WaitForInterrupt()
		{
		}

		/// <summary>
		/// Processes the interrupt.
		/// </summary>
		/// <param name="irq">The irq.</param>
		void IHardwareAbstraction.ProcessInterrupt(byte irq)
		{
			HAL.ProcessInterrupt(irq);
		}

		/// <summary>
		/// Sleeps the specified milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		void IHardwareAbstraction.Sleep(uint milliseconds)
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="count">count</param>
		void IHardwareAbstraction.WaitWithNops(uint count)
		{
		}

		/// <summary>
		/// Allocates the memory.
		/// </summary>
		/// <param name="size">The size.</param>
		/// <param name="alignment">The alignment.</param>
		/// <returns></returns>
		IMemory IMemoryControllerAbstraction.AllocateMemory(uint size, uint alignment)
		{
			return null;
		}

		/// <summary>
		/// Gets the physical address.
		/// </summary>
		/// <param name="memory">The memory.</param>
		/// <returns></returns>
		uint IMemoryControllerAbstraction.GetPhysicalAddress(IMemory memory)
		{
			return memory.Address;
		}

		void IPortControllerAbsctraction.PortOut8(ushort port, byte val)
		{
		}
		byte IPortControllerAbsctraction.PortIn8(ushort port)
		{
			return 0;
		}
	}
}
