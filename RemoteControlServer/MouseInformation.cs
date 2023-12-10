using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    internal class MouseInformation
    {
        public const Int32 CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);


        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        public static Bitmap Capture(int x, int y, int width, int height)
        {
            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(x, y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);

                CURSORINFO cursorInfo;
                cursorInfo.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                if (GetCursorInfo(out cursorInfo))
                {
                    // if the cursor is showing draw it on the screen shot
                    if (cursorInfo.flags == CURSOR_SHOWING)
                    {
                        // we need to get hotspot so we can draw the cursor in the correct position
                        var iconPointer = CopyIcon(cursorInfo.hCursor);
                        int iconX, iconY;

                        if (GetIconInfo(iconPointer, out ICONINFO iconInfo))
                        {
                            // calculate the correct position of the cursor
                            iconX = cursorInfo.ptScreenPos.x - ((int)iconInfo.xHotspot);
                            iconY = cursorInfo.ptScreenPos.y - ((int)iconInfo.yHotspot);

                            // draw the cursor icon on top of the captured screen image
                            DrawIcon(g.GetHdc(), iconX, iconY, cursorInfo.hCursor);

                            // release the handle created by call to g.GetHdc()
                            g.ReleaseHdc();
                        }
                    }
                }
            }
            return bitmap;
        }

        public static (int X, int Y) ScaleCoordinates(int oldImageWidth, int oldImageHeight, int newImageWidth, int newImageHeight, int mouseX, int mouseY)
        {
            double scaleX = (double)newImageWidth / oldImageWidth;
            double scaleY = (double)newImageHeight / oldImageHeight;

            mouseX = (int)(mouseX * scaleX);
            mouseY = (int)(mouseY * scaleY);
            return (mouseX, mouseY);
        }

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        public static void SimulateLeftMouseClick(int X, int Y)
        {
            mouse_event((uint)MouseEventFlags.MouseMove, (uint)X, (uint)Y, 0, 0);
        }

        // Метод для имитации нажатия правой кнопки мыши
        public static void SimulateRightMouseClick(int X, int Y)
        {
        }

        public static void ProcessMouseActions(MouseActions mouseAction, int X, int Y)
        {
            uint dwFlags = 0;

            switch (mouseAction)
            {
                case MouseActions.MouseLeft:
                    dwFlags = (uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp);
                    break;
                case MouseActions.MouseRight:
                    dwFlags = (uint)(MouseEventFlags.MouseRightDown | MouseEventFlags.MouseRightUp);
                    break;
                case MouseActions.MouseDoubleClick:
                    mouse_event((uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp), (uint)X, (uint)Y, 0, 0);
                    mouse_event((uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp), (uint)X, (uint)Y, 0, 0);
                    break;
                case MouseActions.MouseMove:
                    SetCursorPos(X, Y);
                    break;
            }

            mouse_event(dwFlags, (uint)X, (uint)Y, 0, 0);
        }
    }
}
