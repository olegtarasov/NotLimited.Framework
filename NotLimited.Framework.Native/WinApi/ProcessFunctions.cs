using System;
using System.Runtime.InteropServices;

namespace NotLimited.Framework.Native.WinApi
{
	public static class ProcessFunctions
	{
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			All = 0x001F0FFF,
			Terminate = 0x00000001,
			CreateThread = 0x00000002,
			VMOperation = 0x00000008,
			VMRead = 0x00000010,
			VMWrite = 0x00000020,
			DupHandle = 0x00000040,
			SetInformation = 0x00000200,
			QueryInformation = 0x00000400,
			Synchronize = 0x00100000
		}

		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int dwSize, out int lpNumberOfBytesRead);
	}
}