using ColorMine.ColorSpaces;
using ColorMine.ColorSpaces.Comparisons;
using Genetic_Algorithm_For_Image_Recreation.Model.Processing;

namespace Genetic_Algorithm_For_Image_Recreation.Model.GA
{
    internal class Fitness
    {
        public static double CalculateFitness(PixelColor[] sourcePixels, PixelColor[] resultPixels, int numberOfGenes)
        {
            if (sourcePixels.Length != resultPixels.Length)
            {
                return double.MaxValue;
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
                differenceValue += diff * diff;

            }

            double avgDifference= Math.Round(differenceValue / resultPixels.Length, 4);
            double geneReward = Math.Log(1 + numberOfGenes);

            double retValue = avgDifference - geneReward;

            retValue = Math.Max(retValue, 0.0001);

            return Math.Round(retValue, 4);


        }
    }
}
