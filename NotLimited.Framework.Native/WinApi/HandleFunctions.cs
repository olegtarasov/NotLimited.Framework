using System;
using System.Runtime.InteropServices;

namespace NotLimited.Framework.Native.WinApi
{
	public static class HandleFunctions
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr hObject);
	}
}