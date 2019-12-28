using BarRaider.SdTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using static net.darkholme.starcitizen.Keyboard;

namespace net.darkholme.starcitizen.sdPlugin
{
    [PluginActionId("net.darkholme.starcitizen.sdPlugin.shields.reset")]
    public class ShieldResetAction : SimpleHotkeyAction
    {

        public ShieldResetAction(SDConnection connection, InitialPayload payload) : 
            base(connection, payload, DirectXKeyStrokes.DIK_NUMPAD5)
        {

        }
    }
}
