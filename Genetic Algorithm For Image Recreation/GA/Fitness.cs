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

            CieDe2000Comparison cieDe2000Comparison = new CieDe2000Comparison();

            for (int i = 0; i < resultPixels.Length; i++)
            {
                sourcePixel = ImageHandler.convertToRgb(sourcePixels[i]);
                resultPixel = ImageHandler.convertToRgb(resultPixels[i]);
                double diff = cieDe2000Comparison.Compare(sourcePixel, resultPixel);
                differenceValue += (diff * diff);

            }

            double retValue= Math.Round(differenceValue / resultPixels.Length, 4);
            return retValue;


        }
    }
}
