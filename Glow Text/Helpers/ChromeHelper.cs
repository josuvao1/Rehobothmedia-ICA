using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using System.Configuration;
using System.Reflection;

namespace Glow_Text
{

    public class ChromeHelper
    {
        private const int ALT = 0xA4;
        private const int EXTENDEDKEY = 0x1;
        private const int KEYUP = 0x2;
        private const uint Restore = 9;

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
        
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool AllowSetForegroundWindow(int procID);

        [DllImport("user32.dll", SetLastError = true)]

        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [StructLayout(LayoutKind.Sequential)]

        internal struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;

        }



        /// <summary>

        /// http://social.msdn.microsoft.com/Forums/en/csharplanguage/thread/f0e82d6e-4999-4d22-b3d3-32b25f61fb2a

        /// </summary>

        [StructLayout(LayoutKind.Explicit)]

        internal struct MOUSEKEYBDHARDWAREINPUT

        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;

        }



        /// <summary>

        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx

        /// </summary>

        [StructLayout(LayoutKind.Sequential)]

        internal struct HARDWAREINPUT

        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;

        }
        
        /// <summary>

        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx

        /// </summary>

        [StructLayout(LayoutKind.Sequential)]

        internal struct KEYBDINPUT

        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;

        }
        /// <summary>

        /// http://social.msdn.microsoft.com/forums/en-US/netfxbcl/thread/2abc6be8-c593-4686-93d2-89785232dacd

        /// </summary>

        [StructLayout(LayoutKind.Sequential)]

        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;

        }

      

        public void OpenChrome()
        {
            var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\SongLive.html"));
            fileName.Replace('\\', '/');
            fileName = ConfigurationManager.AppSettings["SongLiveMachine"];
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            process.StartInfo.Arguments = $"--start-fullscreen --app={fileName} --window-position=1280,0 --window-size=1280,720 ";
     //       process.StartInfo.Arguments = $"--start-fullscreen --app={ConfigurationManager.AppSettings["SongLiveMachine"]} --window-position=1280,0 --window-size=1280,720 ";
     

            process.Start();
        }

        public void CloseChrome()
        {
            Process[] chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
        }



        public void RefreshChrome()
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            foreach (Process chrome in procsChrome)
            {
                if (chrome.MainWindowHandle != IntPtr.Zero)
                {
                    // Set focus on the window so that the key input can be received.
                    // MessageBox.Show("Are you Sure");
                    if (chrome.MainWindowHandle == GetForegroundWindow()) return;
                    //check if window is minimized           
                    if (IsIconic(chrome.MainWindowHandle))
                    {
                       ShowWindow(chrome.MainWindowHandle, Restore);
                    }               
                    IntPtr hWnd = GetForegroundWindow();
                    uint processID = 0;
                    uint threadID = GetWindowThreadProcessId(hWnd, out processID);
                    int intID = (int)processID;
                    AllowSetForegroundWindow(intID);
                 
                 
                    SetForegroundWindow(chrome.MainWindowHandle);

                    // Create a F5 key press

                    INPUT ip = new INPUT { Type = 1 };
                    ip.Data.Keyboard = new KEYBDINPUT();
                    ip.Data.Keyboard.Vk = (ushort)0x74;  // F5 Key
                    ip.Data.Keyboard.Scan = 0;
                    ip.Data.Keyboard.Flags = 0;
                    ip.Data.Keyboard.Time = 0;
                    ip.Data.Keyboard.ExtraInfo = IntPtr.Zero;


                    var inputs = new INPUT[] { ip };

                    // Send ALT keypress to the window
                    keybd_event((byte)ALT, 0x45, EXTENDEDKEY | 0, 0);                
                    keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);            
                    // Send the keypress to the window
                    SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));

                    // probably need to set focus back to your application here.
                }

            }
        }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public void LeftMouseClick(int xpos, int ypos)
        {
            System.Threading.Thread.Sleep(100);
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
          //  SetCursorPos(720, 720);
        }

        public void SetCursorPosition(int xpos, int ypos)
        {
            
            SetCursorPos(xpos, ypos);
         
            //  SetCursorPos(720, 720);
        }

    }

}
