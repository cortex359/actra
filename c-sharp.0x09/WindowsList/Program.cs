using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsList;
using HWND = IntPtr;

static class Program
{
    class Event
    {
        private DateTime Timestamp;
        public string Description;
        public string Type;

        public Event(string Description, string Type)
        {
            Timestamp = DateTime.Now;
            this.Description = Description;
        }
    }

    class EventList
    {
        private List<Event> List;
        public EventList()
        {
            List = new List<Event>();
        }

        public void add(Event e)
        {
            List.Add(e);
        }

        delegate bool Filter (Event e);

        private List<Event> filterEvents(Filter f)
        {
            var result = from n in this.List where f(n) select n;
            return result.ToList();
        }

        public List<Event> showEvents(string searchStr)
        {
            if (searchStr != "")
            {
                Filter desc = e => e.Description.Contains(searchStr);
            }
            else
            {
                Filter none = e => e;
            }
            return filterEvents(desc);
        }
    }

    static void Main()
    {
        IntPtr lastWindowHandle = 0;
        while (true)
        {
            IntPtr fgWindowHandle = OpenWindowGetter.GetForegroundWindow();
            if (lastWindowHandle != fgWindowHandle)
            {
                var window = from n in OpenWindowGetter.GetOpenWindows() where n.Key == fgWindowHandle select n.Value;
                if (window.Count() > 0)
                {
                    Event e = new Event(window.SingleOrDefault(), "FocusChangeEvent");
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
    
    /// <summary>Contains functionality to get all the open windows.</summary>
    /// @NOTE: Imported from https://www.tcx.be/blog/2006/list-open-windows/
    public static class OpenWindowGetter
    {
        /// <summary>Returns a dictionary that contains the handle and title of all the open windows.</summary>
        /// <returns>A dictionary that contains the handle and title of all the open windows.</returns>
        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND shellWindow = GetShellWindow();
            Dictionary<HWND, string> windows = new Dictionary<HWND, string>();

            EnumWindows(delegate(HWND hWnd, int lParam)
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
        public static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

    }
}
