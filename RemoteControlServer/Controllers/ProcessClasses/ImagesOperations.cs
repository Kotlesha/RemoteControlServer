using System.Drawing.Imaging;
using Encoder = System.Drawing.Imaging.Encoder;

namespace RemoteControlServer.Controllers.ProcessClasses
{
    internal static class ImagesOperations
    {
        public static string GetImage(long quality = 50)
        {
            Bitmap screenshot = TakeScreenshot();
            using MemoryStream stream = CompressImage(screenshot, quality);
            return GetResultImageString(stream);
        }

        private static Bitmap TakeScreenshot()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = MouseOperations.CaptureImageWithCursor(bounds.Width, bounds.Height);
            return screenshot;
        }

        private static MemoryStream CompressImage(Bitmap image, long quality)
        {
            EncoderParameters encoderParams = new(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            ImageCodecInfo codecInfo = ImageCodecInfo.GetImageDecoders()
                .First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            MemoryStream ms = new();
            image.Save(ms, codecInfo, encoderParams);
            return ms;
        }

        private static string GetResultImageString(MemoryStream ms)
        {
            ms.Position = 0;
            byte[] bytes = ms.ToArray();
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }
    }
}
