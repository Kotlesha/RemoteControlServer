using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControlServer
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
