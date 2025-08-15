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

        public static String Test(BitmapImage bitmapImage)
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

        public static String RectangleScanning(BitmapImage bitmapImage, Chromosome individual)
        {

            




            return "Nothing";
        }


    }
}
