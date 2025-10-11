using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class ImageHandler
    {
        public static PixelColor[] GetPxielFromResultRectangle(RenderTargetBitmap bitmapImage, Gene gene)
        {

            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;

            int X = (int)gene.X;
            int Y = (int)gene.Y;
             int width = (int)Math.Ceiling(gene.width);
            int height = (int)Math.Ceiling(gene.height);

            byte[] pixelData = new byte[bitmapHeight * stride];

            PixelColor[] pixelColors = new PixelColor[width * height];
            int pixelColorIndex = 0;

            bitmapImage.CopyPixels(pixelData, stride, 0);


            if (X < 0 || Y < 0 || X + width > bitmapWidth || Y + height > bitmapHeight)
            {
                Debug.WriteLine($"Gene out of bounds X={X} Y={Y} W={width} H ={height}");
                return Array.Empty<PixelColor>();
            }


            for (int j = Y; j < Y + height; j++)
            {
                for (int i = X; i < X + width; i++)
                {
                    int pixelIndex = (j * stride + i * bytesPerPixel);
                    byte bluetmp = pixelData[pixelIndex];
                    byte greentmp = pixelData[pixelIndex + 1];
                    byte redtmp = pixelData[pixelIndex + 2];
                    byte alphatmp = pixelData[pixelIndex + 3];

                        pixelColors[pixelColorIndex] = new PixelColor(bluetmp, greentmp, redtmp, alphatmp);
                        pixelColorIndex++;
                        // Debug.WriteLine($"R={redtmp} G={greentmp} B={bluetmp} A={alphatmp} posXY={i}{j} index={pixelIndex}");
                }
            }
            return pixelColors;
        }



        public static PixelColor[] GetPixelFromSourceRectangle(FormatConvertedBitmap bitmapImage, Gene gene)
        {

            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;

            int X = (int)gene.X;
            int Y = (int)gene.Y;
            int width = (int)Math.Ceiling(gene.width);
            int height = (int)Math.Ceiling(gene.height);

            int pixelColorIndex = 0;

            byte[] pixelData = new byte[bitmapHeight * stride];

            PixelColor[] pixelColors = new PixelColor[width * height];

            bitmapImage.CopyPixels(pixelData, stride, 0);


            if(X < 0 || Y < 0 || X + width > bitmapWidth || Y + height > bitmapHeight)
            {
                Debug.WriteLine($"Gene out of bounds X={X} Y={Y} W={width} H ={height}");
                return Array.Empty<PixelColor>();
            }


            for(int j = Y; j < Y + height; j++)
            {
                for (int i = X; i < X + width; i++)
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
