using System.Runtime.InteropServices;

namespace RemoteControlServer.Controllers.ProcessClasses
{
    public class KeyboardOperations
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const int KEYEVENTF_KEYDOWN = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;

        public static async Task SimulateKeyPress(byte keyCode)
        {
            keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            await Task.Delay(100);
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
    }
}
