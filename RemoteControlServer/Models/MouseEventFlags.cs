namespace RemoteControlServer.Models
{
    [Flags]
    public enum MouseEventFlags : uint
    {
        MouseMove = 0x0001,
        MouseLeftDown = 0x02,
        MouseLeftUp = 0x04,
        MouseRightDown = 0x08,
        MouseRightUp = 0x10
    }
}
