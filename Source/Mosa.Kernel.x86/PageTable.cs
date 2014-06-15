﻿/*
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
#region CR0
		// http://en.wikipedia.org/wiki/Control_register
		const uint PG = 1u << 31; // Paging 	If 1, enable paging and use the CR3 register, else disable paging
		const uint CD = 1u << 30; // Cache disable 	Globally enables/disable the memory cache
		const uint NW = 1u << 29; // Not-write through 	Globally enables/disable write-back caching
		const uint AM = 1u << 18; // Alignment mask 	Alignment check enabled if AM set, AC flag (in EFLAGS register) set, and privilege level is 3
		const uint WP = 1u << 16; // Write protect 	Determines whether the CPU can write to pages marked read-only
		const uint NE = 1u << 5; // Numeric error 	Enable internal x87 floating point error reporting when set, else enables PC style x87 error detection
		const uint ET = 1u << 4; // Extension type 	On the 386, it allowed to specify whether the external math coprocessor was an 80287 or 80387
		const uint TS = 1u << 3; // Task switched 	Allows saving x87 task context upon a task switch only after x87 instruction used
		const uint EM = 1u << 2; // Emulation 	If set, no x87 floating point unit present, if clear, x87 FPU present
		const uint MP = 1u << 1; // Monitor co-processor 	Controls interaction of WAIT/FWAIT instructions with TS flag in CR0
		const uint PE = 1u << 0; // Protected Mode Enable 	If 1, system is in protected mode, else system is in real mode
#endregion


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
		public static void Setup()
		{
			uint numberOfPages = 1u << (32 - 12);
			//Panic.Write (0, 1, "numberOfPages");
			//Panic.Number (0, 2, numberOfPages, 10, 10);

			uint totalSizeOfAllPagePointers = numberOfPages * sizeOfPageTableEntry;
			//Panic.Write (0, 7, "totalSizeOfAllPagePointers");
			//Panic.Number (0, 8, totalSizeOfAllPagePointers, 10, 10);
			uint numberOfPageTablePages = (totalSizeOfAllPagePointers + PageFrameAllocator.PageSize - 1) / PageFrameAllocator.PageSize;
			uint numberOfDirectoryPages = (numberOfPageTablePages  + numberOfPageDirectoryEntriesPerPage - 1)/ numberOfPageDirectoryEntriesPerPage;

			// Setup Page Directory
			for (uint uindex = 0; uindex < numberOfPageTablePages; uindex++)
			{
				uint addressOfPageTablePage = pageTable + uindex * PageFrameAllocator.PageSize;
				//Panic.Write (40, 15, "addressOfPageTablePage");
				//Panic.Number (70, 15, addressOfPageTablePage, 16, 8);

				uint dirEntryAddr = pageDirectory + uindex * sizeOfPageDirectoryEntry;
				//Panic.Write (40, 16, "dirEntryAddr");
				//Panic.Number (70, 16, dirEntryAddr, 16, 8);

				uint dirEntryVal = MakePageDirectoryEntry (addressOfPageTablePage);
				//Panic.Write (40, 17, "dirEntryVal");
				//Panic.Number (70, 17, dirEntryVal, 16, 8);

				Native.Set32 (dirEntryAddr, dirEntryVal);
			}
			//Panic.Write (0, 3, "numberOfDirectoryPages");
			//Panic.Number (0, 4, numberOfDirectoryPages, 10, 10);
			//Panic.Write (0, 5, "numberOfPageTablePages");
			//Panic.Number (0, 6, numberOfPageTablePages, 10, 10);

			// Map 4G of memory
			for (uint uPage = 0; uPage < numberOfPages; uPage++)
			{
				uint addressOfMemoryPage = uPage * PageFrameAllocator.PageSize;
				//Panic.Write (40, 20, "addressOfMemoryPage");
				//Panic.Number (70, 20, addressOfMemoryPage, 16, 8);

				uint uDirEntry = uPage / numberOfPageDirectoryEntriesPerPage;
				uint uPageEntryIndex = uPage - uDirEntry * numberOfPageDirectoryEntriesPerPage;

				uint addressOfPageTableEntry = Native.Get32(pageDirectory + uDirEntry * sizeOfPageDirectoryEntry) + uPageEntryIndex * sizeOfPageTableEntry;
				//Panic.Write (40, 19, "addressOfPageTableEntry");
				//Panic.Number (70, 19, addressOfPageTableEntry, 16, 8);

				uint entry = MakePageTableEntry (addressOfMemoryPage);
				//Panic.Write (40, 21, "entry");
				//Panic.Number (70, 21, entry, 16, 8);
				Native.Set32 (addressOfPageTableEntry, entry);
			}

			// Set CR3 register on processor - sets page directory
			Native.SetCR3(pageDirectory);

			// Set CR0 register on processor - turns on virtual memory
			// It is enabled by setting the PG bit to 1 (left most bit in CR0 ).
			// The paging system operates in both real and protected mode.
			// (If set to 0, linear addresses are physical addresses).
			uint oldCR0 = Native.GetCR0 ();
			Panic.Number (2, 2, oldCR0, 2, 32);
			Native.SetCR0(oldCR0 | PG);
			Panic.Now (127);
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
