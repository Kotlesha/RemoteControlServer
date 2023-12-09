using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RemoteControlServer
{
    internal static class ProcessingRequests
    {
        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);

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
                    Point point = JsonSerializer.Deserialize<Point>(request.JsonData);
                    SetCursorPos(point.X, point.Y);
                    code = 200;
                    response.ResultData = code.ToString();
                    break;

            }

            return response;
        }
    }
}
