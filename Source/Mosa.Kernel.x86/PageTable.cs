/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using Mosa.Platform.Internal.x86;

namespace Mosa.Kernel.x86
{
	/// <summary>
	///
	/// </summary>
	public static class PageTable
	{
		const uint PG = 0x80000000;
		// Location for page directory starts at 20MB
		private static uint pageDirectory = 1024 * 1024 * 20; // 0x1400000

		// Location for page tables start at 16MB
		private static uint pageTable = 1024 * 1024 * 16;	// 0x1000000

		const uint sizeOfPageTableEntry = 4;
		const uint numberOfPageTableEntriesPerPage = PageFrameAllocator.PageSize / sizeOfPageTableEntry;
		const uint sizeOfPageDirectoryEntry = 4;
		const uint numberOfPageDirectoryEntriesPerPage = PageFrameAllocator.PageSize / sizeOfPageDirectoryEntry;

		const uint flagUser = 0x04;
		const uint flagWriteable = 0x02;
		const uint flagPresent = 0x01;

		static uint MakePageDirectoryEntry (uint address)
		{
			//address &= ~0xFFF;
			if ((address & 0xFFF) > 0)
			{
				Panic.Now (18);
			}
			return address | flagUser | flagWriteable | flagPresent;
		}

		static uint MakePageTableEntry (uint address)
		{
			//address &= ~0xFFF;
			if ((address & 0xFFF) > 0)
			{
				Panic.Now (19);
			}
			return address | flagUser | flagWriteable | flagPresent;
		}

		/// <summary>
		/// Sets up the PageTable
		/// </summary>
		public static void Setup(uint numberOfPages)
		{
			Panic.Write (0, 1, "numberOfPages");
			Panic.Number (0, 2, numberOfPages, 10, 10);
			//Panic.Now (124);

			uint totalSizeOfAllPagePointers = numberOfPages * sizeOfPageTableEntry;
			Panic.Write (0, 7, "totalSizeOfAllPagePointers");
			Panic.Number (0, 8, totalSizeOfAllPagePointers, 10, 10);
			uint numberOfPageTablePages = (totalSizeOfAllPagePointers + PageFrameAllocator.PageSize - 1) / PageFrameAllocator.PageSize;
			uint numberOfDirectoryPages = (numberOfPageTablePages  + numberOfPageDirectoryEntriesPerPage - 1)/ numberOfPageDirectoryEntriesPerPage;

			// Setup Page Directory
			for (uint uindex = 0; uindex < numberOfPageTablePages; uindex++)
			{
				uint addressOfPageTablePage = pageTable + uindex * PageFrameAllocator.PageSize;
				Native.Set32 (pageDirectory + uindex * sizeOfPageDirectoryEntry, MakePageDirectoryEntry(addressOfPageTablePage));
			}
			Panic.Write (0, 3, "numberOfDirectoryPages");
			Panic.Number (0, 4, numberOfDirectoryPages, 10, 10);
			Panic.Write (0, 5, "numberOfPageTablePages");
			Panic.Number (0, 6, numberOfPageTablePages, 10, 10);

			// Map the first 128MB of memory (32786 4K pages)
			for (uint uPage = 0; uPage < numberOfPages; uPage++)
			{
				uint addressOfMemoryPage = uPage * PageFrameAllocator.PageSize;
				uint uDirEntry = uPage / numberOfPageDirectoryEntriesPerPage;
				uint uPageEntryIndex = uPage - uDirEntry * numberOfPageDirectoryEntriesPerPage;
				uint addressOfPageTableEntry = Native.Get32(pageDirectory + uDirEntry * sizeOfPageDirectoryEntry) + uPageEntryIndex * sizeOfPageTableEntry;
				Native.Set32 (addressOfPageTableEntry, MakePageTableEntry(addressOfMemoryPage));
			}

			// Set CR3 register on processor - sets page directory
			Native.SetCR3(pageDirectory);

			// Set CR0 register on processor - turns on virtual memory
			// It is enabled by setting the PG bit to 1 (left most bit in CR0 ).
			// The paging system operates in both real and protected mode.
			// (If set to 0, linear addresses are physical addresses).
			Native.SetCR0(Native.GetCR0() | PG);
			//Panic.Now (126);
		}

		/// <summary>
		/// Maps the virtual address to physical.
		/// </summary>
		/// <param name="virtualAddress">The virtual address.</param>
		/// <param name="physicalAddress">The physical address.</param>
		public static void MapVirtualAddressToPhysical(uint virtualAddress, uint physicalAddress)
		{
			Native.Set32(pageTable + ((virtualAddress / PageFrameAllocator.PageSize) * sizeOfPageTableEntry), MakePageTableEntry(physicalAddress));
		}

		/// <summary>
		/// Gets the physical memory.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public static uint GetPhysicalAddressFromVirtual(uint address)
		{
			return Native.Get32(pageTable + ((address / PageFrameAllocator.PageSize) * sizeOfPageTableEntry)) & 0xFFF;
		}
	}
}
