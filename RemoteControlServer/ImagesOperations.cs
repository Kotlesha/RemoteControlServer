using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RemoteControlServer
{
    internal static class ImagesOperations
    {
        public static string GetImage(long quality = 50)
        {
            Bitmap screenshot = TakeScreenshot();
            MemoryStream stream = CompressImage(ref screenshot, quality);
            return GetResultImageString(ref stream);
        }

        private static Bitmap TakeScreenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            MouseInformation.GetCursorInfo(out MouseInformation.CURSORINFO cursor);
            Bitmap screenshot = MouseInformation.Capture(cursor.ptScreenPos.x, cursor.ptScreenPos.y, bounds.Width, bounds.Height);
            return screenshot;
        }

        private static MemoryStream CompressImage(ref Bitmap image, long quality)
        {
            EncoderParameters encoderParams = new(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders()
                .First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            MemoryStream ms = new();
            image.Save(ms, codecInfo, encoderParams);
            return ms;
        }

        private static string GetResultImageString(ref MemoryStream ms)
        {
            ms.Position = 0;
            byte[] bytes = ms.ToArray();
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }
    }
}
