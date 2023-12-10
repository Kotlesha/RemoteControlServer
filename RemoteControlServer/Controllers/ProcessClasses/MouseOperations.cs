using RemoteControlServer.Models;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace RemoteControlServer.Controllers.ProcessClasses
{
    internal class MouseOperations
    {
        private const int CURSOR_SHOWING = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        private struct ICONINFO
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        private static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        private static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        private static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);


        public static Bitmap CaptureImageWithCursor(int width, int height)
        {
            GetCursorInfo(out CURSORINFO Cursor);
            int x = Cursor.ptScreenPos.x, y = Cursor.ptScreenPos.y;
            var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(x, y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);

                CURSORINFO cursorInfo;
                cursorInfo.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                if (GetCursorInfo(out cursorInfo))
                {
                    if (cursorInfo.flags == CURSOR_SHOWING)
                    {
                        var iconPointer = CopyIcon(cursorInfo.hCursor);
                        int iconX, iconY;

                        if (GetIconInfo(iconPointer, out ICONINFO iconInfo))
                        {
                            iconX = cursorInfo.ptScreenPos.x - iconInfo.xHotspot;
                            iconY = cursorInfo.ptScreenPos.y - iconInfo.yHotspot;
                            DrawIcon(g.GetHdc(), iconX, iconY, cursorInfo.hCursor);
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

        public static void ProcessMouseActions(MouseState mouseAction, int X, int Y)
        {
            uint dwFlags = 0;

            switch (mouseAction)
            {
                case MouseState.MouseLeft:
                    dwFlags = (uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp);
                    break;
                case MouseState.MouseRight:
                    dwFlags = (uint)(MouseEventFlags.MouseRightDown | MouseEventFlags.MouseRightUp);
                    break;
                case MouseState.MouseDoubleClick:
                    mouse_event((uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp), (uint)X, (uint)Y, 0, 0);
                    mouse_event((uint)(MouseEventFlags.MouseLeftDown | MouseEventFlags.MouseLeftUp), (uint)X, (uint)Y, 0, 0);
                    break;
                case MouseState.MouseMove:
                    SetCursorPos(X, Y);
                    break;
            }

            mouse_event(dwFlags, (uint)X, (uint)Y, 0, 0);
        }
    }
}
