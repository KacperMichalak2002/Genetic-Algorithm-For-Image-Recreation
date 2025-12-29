using Genetic_Algorithm_For_Image_Recreation.Model.GA;
using Genetic_Algorithm_For_Image_Recreation.Model.Processing;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
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


            byte newR = (byte)(redTmp * newAlphaVal + oldColor.R * oneMinusAlpha);
            byte newG = (byte)(greenTmp * newAlphaVal + oldColor.G * oneMinusAlpha);
            byte newB = (byte)(blueTmp * newAlphaVal + oldColor.B * oneMinusAlpha);


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

        private static double TriangleArea(BasicPoint point1, BasicPoint point2, BasicPoint point3)
        {
            return Math.Abs(
                (point1.X * (point2.Y - point3.Y) +
                point2.X * (point3.Y - point1.Y) +
                point3.X * (point1.Y - point2.Y)) / 2.0);
        }

        private static bool isInsideTriangle(BasicPoint point1, BasicPoint point2, BasicPoint point3, BasicPoint pointToCheck)
        {
            double ABCTriangle = TriangleArea(point1, point2, point3);
            double PBCTriangle = TriangleArea(pointToCheck, point2, point3);
            double PACTriangle = TriangleArea(point1, pointToCheck, point3);
            double PABTriangle = TriangleArea(point1, point2, pointToCheck);

            double sumOfTriangles = PBCTriangle + PACTriangle + PABTriangle;

            const double epsilon = 0.0001;

            return Math.Abs(ABCTriangle - sumOfTriangles) < epsilon;
        }



        private static void PixelsFromTriangle(PixelColor[] pixels, Gene gene, int imageWidth, int imageHeight)
        {

            int geneWidth = gene.width;
            int geneHeight = gene.height;
            int startX = gene.X;
            int startY = gene.Y;

            int endX = startX + geneWidth;
            int endY = startY + geneHeight;

            BasicPoint point1 = gene.points[0];
            BasicPoint point2 = gene.points[1];
            BasicPoint point3 = gene.points[2];
            BasicPoint pointToChech;

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    pointToChech = new BasicPoint(i, j);
                    if (isInsideTriangle(point1, point2, point3, pointToChech))
                    {
                        int index = j * imageWidth + i;

                        pixels[index] = BlendColorsWithAlpha(pixels[index], gene.color);
                    }
                }
            }

        }
    }
}
