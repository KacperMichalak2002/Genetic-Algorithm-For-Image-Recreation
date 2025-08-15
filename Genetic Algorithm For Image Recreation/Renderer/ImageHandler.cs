using Genetic_Algorithm_For_Image_Recreation.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class ImageHandler
    {

        public static String GetPixelFromSource(BitmapImage bitmapImage)
        {

            int height = bitmapImage.PixelHeight;
            int width = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = width * bytesPerPixel;

            byte[] pixelData = new byte[height * width * bytesPerPixel];

            bitmapImage.CopyPixels(pixelData, stride, 0);

            byte blue = pixelData[0];
            byte green = pixelData[1];
            byte red = pixelData[2];
            byte alpha = pixelData[3];


            return $"R={red} G={green} B={blue} A={alpha}";
        }

        public static String GetPxielFromResult(RenderTargetBitmap bitmapImage)
        {

            int height = bitmapImage.PixelHeight;
            int width = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = width * bytesPerPixel;

            byte[] pixelData = new byte[height * width * bytesPerPixel];

            bitmapImage.CopyPixels(pixelData, stride, 0);

            List<byte> pixels = new List<byte>();

            for (int i = 0; i < pixelData.Length; i += bytesPerPixel)
            {
                byte alpha = pixelData[i + 3];

                if (alpha > 0)
                {
                    byte blue = pixelData[i];
                    byte green = pixelData[i + 1];
                    byte red = pixelData[i + 2];

                    blue = (byte)(blue * 255 / alpha);
                    green = (byte)(green * 255 / alpha);
                    red = (byte)(red * 255 / alpha);

                    return $"R={red} G={green} B={blue} A={alpha}";
                }
            }

            return "Nothing";

        }

        public static String RectangleScanning(ImageSource image, Chromosome chromosome)
        {

            




            return "Nothing";
        }


    }
}
