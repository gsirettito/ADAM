using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SiretT.Interop {
    public class User32 {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpMousePoint);

        public struct POINT {
            public int X { get; set; }

            public int Y { get; set; }
        }

        public static class GlobalMouse {
            public static Point Position {
                get {
                    POINT pos;
                    User32.GetCursorPos(out pos);
                    return new Point(pos.X, pos.Y);
                }
                set {
                    User32.SetCursorPos((int)value.X, (int)value.Y);
                }
            }
        }
    }
}
