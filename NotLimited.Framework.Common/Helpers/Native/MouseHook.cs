using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NotLimited.Framework.Common.Helpers.Native
{
    public class MouseMoveEventArgs : EventArgs
    {
        public MouseMoveEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class MouseHook : IDisposable
    {
        private IntPtr _hookId = IntPtr.Zero;

        public event EventHandler<MouseMoveEventArgs> MouseMove;
        public event EventHandler LButtonUp;

        private void OnLButtonUp(EventArgs e)
        {
            if (LButtonUp != null) 
                LButtonUp(this, e);
        }

        private void OnMouseMove(MouseMoveEventArgs e)
        {
            if (MouseMove != null) 
                MouseMove(null, e);
        }


        public void SetHook()
        {
            _hookId = SetHook(HookCallback);
        }

        public void RemoveHook()
        {
            if (_hookId != IntPtr.Zero)
            {
                if (!UnhookWindowsHookEx(_hookId))
                    throw new InvalidOperationException("Failed to remove the hook!");

                _hookId = IntPtr.Zero;
            }
        }

        public PointApi GetCursorPos()
        {
            PointApi result;
            GetCursorPos(out result);
            return result;
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_MOUSEMOVE:
                        OnMouseMove(new MouseMoveEventArgs(hookStruct.pt.x, hookStruct.pt.y));
                        break;
                    case MouseMessages.WM_LBUTTONUP:
                        OnLButtonUp(EventArgs.Empty);
                        break;
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;

        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public PointApi pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
                                                      LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                                                    IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out PointApi lpPoint);

        public void Dispose()
        {
            RemoveHook();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PointApi
    {
        public int x;
        public int y;
    }
}