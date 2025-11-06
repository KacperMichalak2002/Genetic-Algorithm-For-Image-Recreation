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
            ShapeType shapeType = individual.Chromosome.shapeType;

            PixelColor[] pixels = new PixelColor[imageWidth * imageHeight];
            PixelColor backgroundColorTemp = new PixelColor(255, 255, 255, 255);
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = backgroundColorTemp;
            }

            foreach(Gene gene in individual.Chromosome.genes)
            {
                switch (shapeType)
                {
                    case ShapeType.Rectangle:
                        PixelsFromRectangle(pixels, gene, imageWidth, imageHeight);
                        break;
                    case ShapeType.Ellipse:
                        PixelsFromEllipse(pixels, gene, imageWidth, imageHeight);
                        break;

                }
                
            }

            return pixels;
        }

        private static void PixelsFromRectangle(PixelColor[] pixels,Gene gene, int imageWidth, int imageHeight)
        {
            int geneWidth = gene.width;
            int geneHeight = gene.height;
            int startX = gene.X;
            int startY = gene.Y;

            int endX = startX + geneWidth;
            int endY = startY + geneHeight;

            byte redTmp = gene.color.R;
            byte greenTmp = gene.color.G;
            byte blueTmp = gene.color.B;
            byte alphaTmp = gene.color.A;

            PixelColor colorTmp = new PixelColor(blueTmp, greenTmp, redTmp, alphaTmp);

            for (int i = startX; i < endX; i++)
            {
                for(int j = startY; j < endY; j++)
                {
                    int index = j * imageWidth + i;
                    pixels[index] = colorTmp;
                }
            }
        }
        private static void PixelsFromEllipse(PixelColor[] pixels, Gene gene, int imageWidth, int imageHeight)
        { 
            int radiusX = gene.width / 2;
            int radiusY = gene.height / 2;

            if (radiusX == 0 || radiusY == 0)
                return;

            int centerX = gene.X + radiusX;
            int centerY = gene.Y + radiusY;
            double radiusXSquare = (double)Math.Pow(radiusX, 2);
            double radiusYSquare = (double)Math.Pow(radiusY, 2);

            int startX = gene.X;
            int startY = gene.Y;

            int endX = startX + gene.width;
            int endY = startY + gene.height;

            byte redTmp = gene.color.R;
            byte greenTmp = gene.color.G;
            byte blueTmp = gene.color.B;
            byte alphaTmp = gene.color.A;


            PixelColor colorTmp = new PixelColor(blueTmp, greenTmp, redTmp, alphaTmp);

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {

                    double valueCheck = Math.Pow(i - centerX, 2) / radiusXSquare +
                                        Math.Pow(j - centerY, 2) / radiusYSquare;

                    if(valueCheck <= 1)
                    {
                        int index = j * imageWidth + i;
                        pixels[index] = colorTmp;
                    } 
                }
            }
        }
    }
}
