﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mosa.Test.Collection
{
	class ArrayLayoutTests
	{
		public unsafe static bool B()
		{
			bool[] arr = new bool[] { true, false, true, false };
			return arr[0] && !arr[1] && arr[2] && !arr[3];
		}

		public unsafe static bool C()
		{
			char[] arr = new char[] { 'W', 'X', 'Y', 'Z' };
			return arr[0] == 'W' && arr[1] == 'X' && arr[2] == 'Y' && arr[3] == 'Z';
		}

		public unsafe static bool U1()
		{
			byte[] arr = new byte[] { 0x55, 0x80, 0xaa, 0xff };
			return arr[0] == 0x55 && arr[1] == 0x80 && arr[2] == 0xaa && arr[3] == 0xff;
		}

		public unsafe static bool U2()
		{
			ushort[] arr = new ushort[] { 0x5555, 0x8080, 0xaaaa, 0xffff };
			return arr[0] == 0x5555 && arr[1] == 0x8080 && arr[2] == 0xaaaa && arr[3] == 0xffff;
		}

		public unsafe static bool U4()
		{
			uint[] arr = new uint[] { 0x55555555, 0x80808080, 0xaaaaaaaa, 0xffffffff };
			return arr[0] == 0x55555555 && arr[1] == 0x80808080 && arr[2] == 0xaaaaaaaa && arr[3] == 0xffffffff;
		}

		public unsafe static bool U8()
		{
			ulong[] arr = new ulong[] { 0x5555555555555555, 0x8080808080808080, 0xaaaaaaaaaaaaaaaa, 0xffffffffffffffff };
			return arr[0] == 0x5555555555555555 && arr[1] == 0x8080808080808080 && arr[2] == 0xaaaaaaaaaaaaaaaa && arr[3] == 0xffffffffffffffff;
		}

		public unsafe static bool R4()
		{
			float[] arr = new float[] { 1.234f, 5.678f, 9.012f, 3.456f };
			return arr[0] == 1.234f && arr[1] == 5.678f && arr[2] == 9.012f && arr[3] == 3.456f;
		}

		public unsafe static bool R8()
		{
			double[] arr = new double[] { 1.23456, 7.89012, 3.45678, 9.01234 };
			return arr[0] == 1.23456 && arr[1] == 7.89012 && arr[2] == 3.45678 && arr[3] == 9.01234;
		}
	}
}
