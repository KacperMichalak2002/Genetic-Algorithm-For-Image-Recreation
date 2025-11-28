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

        public static PixelColor GetBackrgoundColor(PixelColor[] imagePixels)
        {

            long sumR = 0;
            long sumG = 0;
            long sumB = 0;
            long sizeOfImage = imagePixels.Length;

            for(int i = 0; i < sizeOfImage; i++)
            {
                sumR += imagePixels[i].R;
                sumG += imagePixels[i].G;
                sumB += imagePixels[i].B;
            }

            byte avgR = (byte)(sumR / sizeOfImage);
            byte avgG = (byte)(sumG / sizeOfImage);
            byte avgB = (byte)(sumB / sizeOfImage);

            return new PixelColor(avgB, avgG, avgR, 255);
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


    public struct PixelColor
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }

        public PixelColor(byte b, byte g, byte r, byte a)
        {
            B = b;
            G = g;
            R = r;
            A = a;
        }

        public override string ToString() => $"({R},{G},{B}) A{A}";

    }

}
