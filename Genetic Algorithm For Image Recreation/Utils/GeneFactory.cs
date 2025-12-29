using Genetic_Algorithm_For_Image_Recreation.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    public class GeneFactory
    {
        public static Gene GenerateGene(GeneFactoryConfig geneFactoryConfig, int imageWidth, int imageHeight, Random random)
        {

            int maxWidth = geneFactoryConfig.maxWidth;
            int maxHeight = geneFactoryConfig.maxHeight;

            int geneWidth = random.Next(1, maxWidth + 1);
            int geneHeight = random.Next(1, maxHeight + 1);


            int maxValueOfX = imageWidth - geneWidth;
            int maxValueOfY = imageHeight - geneHeight;

            int geneX = random.Next(0, maxValueOfX);
            int geneY = random.Next(0, maxValueOfY);
            
            ShapeType shapeType = geneFactoryConfig.shapeType;

            Gene gene = new Gene
            {
                ShapeType = shapeType,
                X = geneX,
                Y = geneY,
                width = geneWidth,
                height = geneHeight,
                //color = System.Windows.Media.Color.FromRgb(255, 0, 0),
                color = System.Windows.Media.Color.FromArgb(
                    (byte)random.Next(geneFactoryConfig.minAlpha, geneFactoryConfig.maxAlpha + 1),
                    (byte)random.Next(256),
                    (byte)random.Next(256),
                    (byte)random.Next(256)),
                //color = System.Windows.Media.Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))
                //backgroundColor = System.Windows.Media.Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
            };

            switch (shapeType)
            {
                case ShapeType.Ellipse:
                case ShapeType.Rectangle:
                    break;
                case ShapeType.Triangle:
                    BasicPoint point1 = new BasicPoint(geneX + random.Next(0, geneWidth), geneY + random.Next(0, geneHeight));
                    BasicPoint point2 = new BasicPoint(geneX + random.Next(0, geneWidth), geneY + random.Next(0, geneHeight));
                    BasicPoint point3 = new BasicPoint(geneX + random.Next(0, geneWidth), geneY + random.Next(0, geneHeight));

                    gene.points.Add(point1);
                    gene.points.Add(point2);
                    gene.points.Add(point3);
                    break;
            }

            return gene;
        }
    }
}
