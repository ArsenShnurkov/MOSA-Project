/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.DeviceSystem
{
	/// <summary>
	///
	/// </summary>
	public interface IHardwareAbstraction : IInterruptControllerAbstraction, IMemoryControllerAbstraction, IPortControllerAbsctraction
	{
		/// <summary>
		/// Disables all interrupts.
		/// </summary>
		void DisableAllInterrupts();

		/// <summary>
		/// Enables all interrupts.
		/// </summary>
		void EnableAllInterrupts();

		/// <summary>
		/// Processes the interrupt.
		/// </summary>
		/// <param name="irq">The irq.</param>
		void ProcessInterrupt(byte irq);

		/// <summary>
		/// Sleeps the specified milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		void Sleep(uint milliseconds);

		/// <summary>
		/// Hlt instruction
		/// </summary>
		void WaitForInterrupt();

		void WaitWithNops(uint count);
	}
}
