using BarRaider.SdTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;
using WindowsInput.Native;
using static net.darkholme.starcitizen.Keyboard;

namespace net.darkholme.starcitizen.sdPlugin
{
    public abstract class SimpleHotkeyAction: PluginBase
    {
        //[DllImport("user32.dll")]
        //public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll", SetLastError = true)]
        //private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);


        //const uint WM_KEYDOWN = 0x100;
        //const uint WM_KEYUP = 0x0101;

        static InputSimulator Input = new InputSimulator();

        static Process StarCitizenProcess;
        static IntPtr? WindowHandle {
            get {
                if(StarCitizenProcess==null || StarCitizenProcess.HasExited)
                {
                    StarCitizenProcess = null;
                    Process[] processList = Process.GetProcesses();

                    foreach (Process P in processList)
                    {
                        if (P.ProcessName.Equals("StarCitizen"))
                        {
                            StarCitizenProcess = P;
                        }
                    }
                    if(StarCitizenProcess==null)
                    {
                        Logger.Instance.LogMessage(TracingLevel.WARN, "StarCitizen Process Not Found");
                    }
                }
                return StarCitizenProcess?.MainWindowHandle;
            }
        }

        

        private readonly IEnumerable<DirectXKeyStrokes> KeySequence;


        public SimpleHotkeyAction(SDConnection connection, InitialPayload payload,
            params DirectXKeyStrokes[] KeySequence
            ) : base(connection, payload)
        {
            this.KeySequence = KeySequence;
        }

        public override void Dispose()
        {
        }

        public override void KeyPressed(KeyPayload payload)
        {
            if(WindowHandle.HasValue)
            {
                foreach(var key in KeySequence)
                {

                    Logger.Instance.LogMessage(TracingLevel.DEBUG, $"Pressing key {key.ToString()}");
                    Keyboard.SendKey(key, false, Keyboard.InputType.Keyboard);
                    //Input.Keyboard.KeyDown(key);
                }
            } else
            {
                Logger.Instance.LogMessage(TracingLevel.WARN, "StarCitizen not found, skipping key presses");
            }
        }
        
        public override void KeyReleased(KeyPayload payload)
        {
            if (WindowHandle.HasValue)
            {
                foreach (var key in KeySequence.Reverse())
                {
                    Logger.Instance.LogMessage(TracingLevel.DEBUG, $"Releasing key {key.ToString()}");
                    Keyboard.SendKey(key ,true, Keyboard.InputType.Keyboard);
                    //Input.Keyboard.KeyPress(key);
                }
            }
            else
            {
                Logger.Instance.LogMessage(TracingLevel.WARN, "StarCitizen not found, skipping key releases");
            }
        }

        public override void OnTick()
        {
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload)
        {
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
        }
    }
}
