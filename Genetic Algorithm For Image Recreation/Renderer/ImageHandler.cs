using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class ImageHandler
    {
        public static PixelColor[] GetAllPxielsFromBitmap(BitmapSource bitmapImage)
        {
            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;


            byte[] pixelData = new byte[bitmapHeight * stride];

            PixelColor[] pixelColors = new PixelColor[bitmapWidth * bitmapHeight];
            int pixelColorIndex = 0;

            bitmapImage.CopyPixels(pixelData, stride, 0);

            for (int j = 0; j < bitmapHeight; j++)
            {
                for (int i = 0; i < bitmapWidth; i++)
                {
                    int pixelIndex = (j * stride + i * bytesPerPixel);
                    byte bluetmp = pixelData[pixelIndex];
                    byte greentmp = pixelData[pixelIndex + 1];
                    byte redtmp = pixelData[pixelIndex + 2];
                    byte alphatmp = pixelData[pixelIndex + 3];

                    pixelColors[pixelColorIndex] = new PixelColor(bluetmp, greentmp, redtmp, alphatmp);
                    pixelColorIndex++;
                }
            }
            return pixelColors;

        }

        public static Rgb convertToRgb(PixelColor pixel)
        {
            return new Rgb
            {
                R = pixel.R,
                G = pixel.G,
                B = pixel.B,
            };
        }
    }

    public class PixelColor
    {
        public byte B { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }
        public byte A { get; set; }

        public PixelColor(byte B, byte G, byte R, byte A)
        {
            this.B = B;
            this.G = G;
            this.R = R;
            this.A = A;
        }

        public override string ToString()
        {
            return $"({R},{G},{B}) A{A}";
        }
       

    }
}
