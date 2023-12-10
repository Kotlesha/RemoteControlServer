using RemoteControlServer.Controllers.ProcessClasses;
using RemoteControlServer.Models;
using System.Text.Json;

namespace RemoteControlServer.Controllers.ServerClasses
{
    internal static class ProcessingRequests
    {
        public static async Task<Response> GetResponseOnRequest(Request request, Func<string, bool> askPermission = null)
        {
            Response response = new() { IdOperation = request.IdOperation };

            switch (request.IdOperation)
            {
                case 0:
                    string name = JsonSerializer.Deserialize<string>(request.JsonData);
                    bool result = askPermission($"К вам хочет подключится пользователь {name}. Ответь ок для принятия или отмена для того, чтобы отказаться от трансляции своего экрана");
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
                    var point = MouseOperations.ScaleCoordinates(data[0], data[1], screenSize.Width, screenSize.Height, data[2], data[3]);
                    MouseOperations.SetCursorPos(point.X, point.Y);
                    MouseOperations.ProcessMouseActions((MouseState)data[4], point.X, point.Y);
                    code = 200;
                    response.ResultData = code.ToString();
                    break;
                case 3:
                    int keyCode = JsonSerializer.Deserialize<int>(request.JsonData);
                    await KeyboardOperations.SimulateKeyPress((byte)keyCode);
                    code = 200;
                    response.ResultData = code.ToString();
                    break;
                case 4:
                    name = JsonSerializer.Deserialize<string>(request.JsonData);
                    result = askPermission($"Пользователь {name} хочет поговорить с вами. Ответьте ок для начала чата или отмена для того, чтобы отказаться от чата");
                    code = result ? 200 : 403;
                    response.ResultData = code.ToString();
                    break;
                case 5:
                    string message = JsonSerializer.Deserialize<string>(request.JsonData);
                    //TextChatForm form = new();
                    response.ResultData = string.Empty;
                    break;
            }

            return response;
        }
    }
}
