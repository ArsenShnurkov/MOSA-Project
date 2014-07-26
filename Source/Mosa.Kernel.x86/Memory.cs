/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Platform.Internal.x86;
using Mosa.DeviceSystem;

namespace Mosa.Kernel.x86
{
	/// <summary>
	/// Static class of helpful memory functions
	/// </summary>
	public class Memory : IMemory
	{
		private uint address;
		private uint size;

		public Memory(uint address, uint size)
		{
			this.address = address;
			this.size = size;
		}

		/// <summary>
		/// Clears the specified memory area.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="bytes">The bytes.</param>
		public static void Clear(uint start, uint bytes)
		{
			if (bytes % 4 == 0)
			{
				Clear4(start, bytes);
				return;
			}

			for (uint at = start; at < (start + bytes); at++)
				Native.Set8(at, 0);
		}

		public static void Clear4(uint start, uint bytes)
		{
			for (uint at = start; at < (start + bytes); at = at + 4)
				Native.Set32(at, 0);
		}

		/// <summary>
		/// Gets the address.
		/// </summary>
		/// <value>The address.</value>
		uint IMemory.Address { get { return address; } }

		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <value>The size.</value>
		uint IMemory.Size { get { return size; } }

		/// <summary>
		/// Gets or sets the <see cref="System.Byte"/> at the specified index.
		/// </summary>
		/// <value></value>
		byte IMemory.this[uint index]
		{
			get { return Native.Get8(address + index); }
			set { Native.Set8(address + index, value); }
		}

		/// <summary>
		/// Reads the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		byte IMemory.Read8(uint index)
		{
			return Native.Get8(address + index);
		}

		/// <summary>
		/// Writes the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		void IMemory.Write8(uint index, byte value)
		{
			Native.Set8(address + index, value);
		}

		/// <summary>
		/// Reads the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		ushort IMemory.Read16(uint index)
		{
			return Native.Get16(address + index);
		}

		/// <summary>
		/// Writes the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		void IMemory.Write16(uint index, ushort value)
		{
			Native.Set16(address + index, value);
		}

		/// <summary>
		/// Reads the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		uint IMemory.Read32(uint index)
		{
			return Native.Get32(address + index);
		}

		/// <summary>
		/// Writes the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		void IMemory.Write32(uint index, uint value)
		{
			Native.Set32(address + index, value);
		}
		/// <summary>
		/// Writes the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		void IMemory.Write24(uint index, uint value)
		{
			//throw new System.NotImplementedException();
		}

		/// <summary>
		/// Reads the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		uint IMemory.Read24(uint index)
		{
			//throw new System.NotImplementedException();
			return 0;
		}
	}
}
