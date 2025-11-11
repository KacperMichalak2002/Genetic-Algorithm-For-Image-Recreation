using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using Genetic_Algorithm_For_Image_Recreation.Renderer;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Fitness
    {
        public static double CalculateFitness(PixelColor[] sourcePixels, PixelColor[] resultPixels)
        {
            if (sourcePixels.Length != resultPixels.Length)
            {
                return Double.MaxValue;
            }

            double differenceValue = 0;

            Rgb sourcePixel;
            Rgb resultPixel;

            Cie94Comparison cie94Comparison = new Cie94Comparison();

            for (int i = 0; i < resultPixels.Length; i++)
            {
                sourcePixel = ImageHandler.convertToRgb(sourcePixels[i]);
                resultPixel = ImageHandler.convertToRgb(resultPixels[i]);
                double diff = cie94Comparison.Compare(sourcePixel, resultPixel);
                differenceValue += diff;

            }

            double retValue= Math.Round(differenceValue / resultPixels.Length, 4);
            return retValue;


        }
    }
}
