using System.Diagnostics;
using System.Windows.Media.Imaging;

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

        public static String GetPxielFromResultRectangle(RenderTargetBitmap bitmapImage, Gene gene)
        {

            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;

            int X = (int)gene.X;
            int Y = (int)gene.Y;
            int width = (int)gene.width;
            int height = (int)gene.height;

            byte[] pixelData = new byte[bitmapHeight * stride];

            bitmapImage.CopyPixels(pixelData, stride, 0);


            if (X < 0 || Y < 0 || X + width > bitmapWidth || Y + height > bitmapHeight)
            {
                Debug.WriteLine("Cant calculate pixels value out of range");
                return "Error";
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

                    if (bluetmp < 255 || greentmp < 255 || redtmp < 255)
                        Debug.WriteLine($"R={redtmp} G={greentmp} B={bluetmp} A={alphatmp} posXY={i}{j} index={pixelIndex}");

                }
            }

            Debug.WriteLine("FINISHED");

            return "Finished";

        }



        public static String GetPixelFromSourceRectangle(FormatConvertedBitmap bitmapImage, Gene gene)
        {

            int bitmapHeight = bitmapImage.PixelHeight;
            int bitmapWidth = bitmapImage.PixelWidth;
            int bytesPerPixel = (bitmapImage.Format.BitsPerPixel + 7) / 8;
            int stride = bitmapWidth * bytesPerPixel;

            int X = (int)gene.X;
            int Y = (int)gene.Y;
            int width = (int)gene.width;
            int height = (int)gene.height;

            byte[] pixelData = new byte[bitmapHeight * stride];

            bitmapImage.CopyPixels(pixelData, stride, 0);


            if(X < 0 || Y < 0 || X + width > bitmapWidth || Y + height > bitmapHeight)
            {
                Debug.WriteLine("Cant calculate pixels value out of range");
                return "Error";
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

                    if(bluetmp < 255 || greentmp < 255 || redtmp < 255)
                        Debug.WriteLine($"R={redtmp} G={greentmp} B={bluetmp} A={alphatmp} posXY={i}{j} index={pixelIndex}");

                }
            }

            Debug.WriteLine("FINISHED");

            return "Finished";
        }
    }
}
