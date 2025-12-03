using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    internal class PixelRenderer
    {
        public static PixelColor[] RenderPixelsToArray(Individual individual, PixelColor backgroundColor)
        {
            int imageWidth = individual.Chromosome.imageWidth;
            int imageHeight = individual.Chromosome.imageHeight;
            ShapeType shapeType = individual.Chromosome.shapeType;

            PixelColor[] pixels = new PixelColor[imageWidth * imageHeight];
            PixelColor backgroundColorTemp = backgroundColor;
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
                    case ShapeType.Triangle:
                        PixelsFromTriangle(pixels, gene, imageWidth, imageHeight);
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
            
            for (int i = startX; i < endX; i++)
            {
                for(int j = startY; j < endY; j++)
                {
                    int index = j * imageWidth + i;

                    pixels[index] = BlendColorsWithAlpha(pixels[index], gene.color);
                }
            }
        }

        private static PixelColor BlendColorsWithAlpha(PixelColor oldColor, Color geneColor)
        {
            byte redTmp = geneColor.R;
            byte greenTmp = geneColor.G;
            byte blueTmp = geneColor.B;
            byte alphaTmp = geneColor.A;

            double newAlphaVal = alphaTmp / 255.0;
            double oneMinusAlpha = 1.0 - newAlphaVal;


            byte newR = (byte)((redTmp * newAlphaVal) + (oldColor.R * oneMinusAlpha));
            byte newG = (byte)((greenTmp * newAlphaVal) + (oldColor.G * oneMinusAlpha));
            byte newB = (byte)((blueTmp * newAlphaVal) + (oldColor.B * oneMinusAlpha));


            return new PixelColor(newB, newG, newR, 255);
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

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {

                    double valueCheck = Math.Pow(i - centerX, 2) / radiusXSquare +
                                        Math.Pow(j - centerY, 2) / radiusYSquare;

                    if(valueCheck <= 1)
                    {
                        int index = j * imageWidth + i;
                        pixels[index] = BlendColorsWithAlpha(pixels[index], gene.color);
                    } 
                }
            }
        }

        private static void PixelsFromTriangle(PixelColor[] pixels, Gene gene, int imageWidth, int imageHeight)
        {

            BasicPoint point1 = gene.points[0];
            BasicPoint point2 = gene.points[1];
            BasicPoint point3 = gene.points[2];

            int minX = Math.Min(point1.X, Math.Min(point2.X, point3.X));
            int maxX = Math.Max(point1.X, Math.Max(point2.X, point3.X));
            int minY = Math.Min(point1.Y,Math.Min(point2.Y, point3.Y));
            int maxY = Math.Max(point1.Y, Math.Max(point2.Y, point3.Y));

            int startX = Math.Max(0, minX);
            int startY = Math.Max(0, minY);
            int endX = Math.Min(imageWidth, maxX + 1);
            int endY = Math.Min(imageHeight, maxY + 1);

            if (startX >= endX || startY >= endY)
                return;


            int vector1x = point2.X - point1.X;
            int vector1y = point2.Y - point1.Y;

            int vector2x = point3.X - point2.X;
            int vector2y = point3.Y - point2.Y;

            int vector3x = point1.X - point3.X;
            int vector3y = point1.Y - point3.Y;

            for (int i = startX; i < endX; i++)
            {
                for(int j = startY; j < endY; j++)
                {
                    int vectorP1x = i - point1.X;
                    int vectorP1y = j - point1.Y;

                    int vectorP2x = i - point2.X;
                    int vectorP2y = j - point2.Y;

                    int vectorP3x = i - point3.X;
                    int vectorP3y = j - point3.Y;

                    int edgeCheck12 = (vectorP1x * vector1y) - (vectorP1y * vector1x);
                    int edgeCheck23 = (vectorP2x * vector2y) - (vectorP2y * vector2x);
                    int edgeCheck31 = (vectorP3x * vector3y) - (vectorP3y * vector3x);

                    bool hasNegValue = false;
                    bool hasPosValue = false;

                    if (edgeCheck12 < 0 || edgeCheck23 < 0 || edgeCheck31 < 0)
                        hasNegValue = true;

                    if (edgeCheck12 > 0 || edgeCheck23 > 0 || edgeCheck31 > 0)
                        hasPosValue = true;


                    if (!(hasNegValue && hasPosValue))
                    {
                        int index = j * imageWidth + i;

                        pixels[index] = BlendColorsWithAlpha(pixels[index], gene.color);
                    }
                }
            }

        }
    }
}
