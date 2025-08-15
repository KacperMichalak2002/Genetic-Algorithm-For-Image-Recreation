using Genetic_Algorithm_For_Image_Recreation.GA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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



        public static String GetPixelFromSourceTest(BitmapImage bitmapImage, double X, double Y, double width, double height)
        {

            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;

            byte[] pixelData = new byte[bitmapHeight * bitmapWidth * bytesPerPixel];

            bitmapImage.CopyPixels(pixelData, stride, 0);

            for(double i = X; i < X + width; i++)
            {
                for(double j = Y; j < Y + height; j++)
                {
                    int pixelIndex = (int)(i * bytesPerPixel + j); 
                    byte bluetmp = pixelData[pixelIndex];
                    byte greentmp = pixelData[pixelIndex + 1];
                    byte redtmp = pixelData[pixelIndex + 2];
                    byte alphatmp = pixelData[pixelIndex + 3];

                    Debug.WriteLine($"R={redtmp} G={greentmp} B={bluetmp} A={alphatmp} posXY={i}{j} index={pixelIndex}");

                }
            }


           



            byte blue = pixelData[0];
            byte green = pixelData[1];
            byte red = pixelData[2];
            byte alpha = pixelData[3];





            return $"R={red} G={green} B={blue} A={alpha}";
        }
        public static String RectangleScanningSource(BitmapImage image)
        {

            double width = 2;
            double height = 2;
            double X = 0;
            double Y = 0;

            GetPixelFromSourceTest(image,X,Y,width,height);

            

            return "Nothing";
        }


    }
}
