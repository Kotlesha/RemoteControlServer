using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RemoteControlServer.Keyboard;

namespace RemoteControlServer
{
    internal static class ProcessingRequests
    {
        public static Response GetResponseOnRequest(Request request, Func<string, bool> askPermission = null)
        {
            Response response = new() { IdOperation = request.IdOperation };

            switch (request.IdOperation)
            {
                case 0:
                    string name = JsonSerializer.Deserialize<string>(request.JsonData);
                    bool result = askPermission(name);
                    int code = result ? 200 : 403;
                    response.ResultData = code.ToString();
                    break;
                case 1: 
                    string jsonImage = ImagesOperations.GetImage();
                    response.ResultData = jsonImage;
                    break;
                case 2:
                    int[] data = JsonSerializer.Deserialize<int[]>(request.JsonData);
                    var screenSize = Screen.PrimaryScreen.Bounds;
                    var point = MouseInformation.ScaleCoordinates(data[0], data[1], screenSize.Width, screenSize.Height, data[2], data[3]);
                    MouseInformation.SetCursorPos(point.X, point.Y);
                    MouseInformation.ProcessMouseActions((MouseActions)data[4], point.X, point.Y);
                    code = 200;
                    response.ResultData = code.ToString();
                    break;
                case 3:
                    int keyCode = JsonSerializer.Deserialize<int>(request.JsonData);
                    uint scanCode = MapVirtualKeyEx((uint)keyCode, 0, IntPtr.Zero);
                    Send((ScanCodeShort)scanCode);
                    code = 200;
                    response.ResultData = code.ToString();
                    break;
            }

            return response;
        }
    }
}
