using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;

using HWND = System.IntPtr;
using System.Windows.Markup;
using System.Windows.Controls;

namespace actra
{
    class EventSource
    {
        private Data Data;

        public void Run()
        {
            IntPtr lastWindowHandle = (HWND)0;
            while (true)
            {
                IntPtr fgWindowHandle = Windows.GetForegroundWindow();
                if (lastWindowHandle != fgWindowHandle)
                {
                    var window =
                        from n in Windows.GetOpenWindows()
                        where n.Key == fgWindowHandle
                        select n.Value;
                    if (window.Count() > 0)
                    {
                        Data.eventList.add(new Event("FocusChangeEvent", window.SingleOrDefault()));
                        Console.WriteLine(window.SingleOrDefault());
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                    lastWindowHandle = fgWindowHandle;
                }
            }
        }

        public EventSource(Data data)
        {
            this.Data = data;
            Console.WriteLine("EventSource created.");
        }
    }

    /// <summary>Contains functionality to get all the open windows.</summary>
    /// @NOTE: See Reference https://www.tcx.be/blog/2006/list-open-windows/
    public static class Windows
    {
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate (HWND hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        private delegate bool EnumWindowsProc(HWND hWnd, int lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(HWND hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(HWND hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(HWND hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

    }
}
