/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.DeviceSystem;
using Mosa.Platform.Internal.x86;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public class HardwareAbstraction_x86 : IHardwareAbstraction
	{
		/// <summary>
		/// Requests an IO read/write port interface from the kernel
		/// </summary>
		/// <param name="port">The port number.</param>
		/// <returns></returns>
		IReadWriteIOPort IPortControllerAbsctraction.RequestIOPort(ushort port)
		{
			return new IOPort(port);
		}

		/// <summary>
		/// Requests a block of memory from the kernel
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="size">The size.</param>
		/// <returns></returns>
		IMemory IMemoryControllerAbstraction.RequestPhysicalMemory(uint address, uint size)
		{
			return new Memory(address, size);
		}

		/// <summary>
		/// Disables all interrupts.
		/// </summary>
		void IHardwareAbstraction.DisableAllInterrupts()
		{
			Native.Cli();
		}

		/// <summary>
		/// Enables all interrupts.
		/// </summary>
		void IHardwareAbstraction.EnableAllInterrupts()
		{
			Native.Sti();
		}

		void IHardwareAbstraction.WaitForInterrupt()
		{
			Native.Hlt();
		}

		void IHardwareAbstraction.WaitWithNops(uint count)
		{
			for (uint i = 0; i < count; ++i)
			{
				Native.Nop();
			}
		}

		/// <summary>
		/// Processes the interrupt.
		/// </summary>
		/// <param name="irq">The irq.</param>
		void IHardwareAbstraction.ProcessInterrupt(byte irq)
		{
			Mosa.DeviceSystem.HAL.ProcessInterrupt(irq);
		}

		/// <summary>
		/// Sleeps the specified milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		void IHardwareAbstraction.Sleep(uint milliseconds)
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
			uint address = KernelMemory.AllocateMemory(size);

			return new Memory(address, size);
		}

		/// <summary>
		/// Gets the physical address.
		/// </summary>
		/// <param name="memory">The memory.</param>
		/// <returns></returns>
		uint IMemoryControllerAbstraction.GetPhysicalAddress(IMemory memory)
		{
			return Mosa.Kernel.x86.PageTable.GetPhysicalAddressFromVirtual(memory.Address);
		}

		void IPortControllerAbsctraction.PortOut8(ushort port, byte val)
		{
			Native.Out8(port, val);
		}

		byte IPortControllerAbsctraction.PortIn8(ushort port)
		{
			return Native.In8(port);
		}
	}
}