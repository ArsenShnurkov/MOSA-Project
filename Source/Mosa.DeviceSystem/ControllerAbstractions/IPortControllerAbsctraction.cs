using System;

namespace Mosa.DeviceSystem
{
	public interface IPortControllerAbsctraction
	{
		/// <summary>
		/// Requests an IO read/write port interface from the kernel
		/// </summary>
		/// <param name="port">The port number.</param>
		/// <returns></returns>
		IReadWriteIOPort RequestIOPort(ushort port);

		void PortOut8(ushort port, byte val);
		byte PortIn8(ushort port);
	}
}

