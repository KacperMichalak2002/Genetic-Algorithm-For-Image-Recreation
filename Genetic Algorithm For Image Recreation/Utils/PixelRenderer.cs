using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    internal class PixelRenderer
    {
        public static PixelColor[] RenderPixelsToArray(Individual individual)
        {
            int imageWidth = individual.Chromosome.imageWidth;
            int imageHeight = individual.Chromosome.imageHeight;

            PixelColor[] pixels = new PixelColor[imageWidth * imageHeight];
            PixelColor backgroundColorTemp = new PixelColor(255, 255, 255, 255);
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = backgroundColorTemp;
            }

            foreach(Gene gene in individual.Chromosome.genes)
            {
                PixelsFromRectangle(pixels, gene, imageWidth, imageHeight);
            }

            return pixels;
        }

        private static void PixelsFromRectangle(PixelColor[] pixels,Gene gene, int imageWidth, int imageHeight)
        {
            int geneWidth = gene.width;
            int geneHeight = gene.height;
            int X = gene.X;
            int Y = gene.Y;

            int endX = X + geneWidth;
            int endY = Y + geneHeight;

            byte redTmp = gene.color.R;
            byte greenTmp = gene.color.G;
            byte blueTmp = gene.color.B;
            byte alphaTmp = gene.color.A;

            PixelColor colorTmp = new PixelColor(blueTmp, greenTmp, redTmp, alphaTmp);

            for (int i = X; i < endX; i++)
            {
                for(int j = Y; j < endY; j++)
                {
                    int index = j * imageWidth + i;
                    pixels[index] = colorTmp;
                }
            }
        }
    }
}
