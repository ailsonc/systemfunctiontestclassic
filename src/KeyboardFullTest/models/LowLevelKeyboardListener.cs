using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardFullTest.models
{
    class LowLevelKeyboardListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 261;
        private const int WM_KEYUP = 257;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, int wParam, ref keyboardHookStruct lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public LowLevelKeyboardListener()
        {
            _proc = HookCallback;
        }

        public void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, int wParam, ref LowLevelKeyboardListener.keyboardHookStruct lParam)
        {
            if (nCode >= 0)
            {
                Keycode scanCode = (Keycode)lParam.scanCode;

                if ((wParam == 256 || wParam == 260) && this.OnKeyPressed != null)
                { 
                    OnKeyPressed(this, new KeyPressedArgs(scanCode)); 
                }
            }
            return CallNextHookEx(_hookID, nCode, (IntPtr)wParam, (IntPtr)lParam.vkCode);
        }

        public struct keyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

    }

    public class KeyPressedArgs : EventArgs
    {
        public Keycode KeyPressed { get; private set; }

        public KeyPressedArgs(Keycode key)
        {
            KeyPressed = key;
        }
    }
}
