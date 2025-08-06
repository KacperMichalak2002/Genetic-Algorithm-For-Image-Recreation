using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    
    internal class Chromosome
    {
        public List<Gene> genes {get; set;} = new List<Gene>();

        public Chromosome(int numberOfGenes, double maxWidth, double maxHeight, Shape shape)
        {
            Random random = new Random();

            for(int i = 0; i < numberOfGenes; i++)
            {
                Gene gene = new Gene
                {
                    shapeToDraw = shape,
                    X = random.NextDouble() * maxWidth,
                    Y = random.NextDouble() * maxHeight,
                    color = System.Windows.Media.Color.FromRgb((byte) random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    backgroundColor = System.Windows.Media.Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                };

                switch (shape)
                {
                    case Ellipse:
                    case System.Windows.Shapes.Rectangle:
                        gene.width = random.NextDouble() * maxWidth;
                        gene.height = random.NextDouble() * maxHeight;
                        break;
                    case Polygon:
                        System.Windows.Point point1 = new System.Windows.Point(random.NextDouble() * maxWidth, random.NextDouble() * maxHeight);
                        System.Windows.Point point2 = new System.Windows.Point(random.NextDouble() * maxWidth, random.NextDouble() * maxHeight);
                        System.Windows.Point point3 = new System.Windows.Point(random.NextDouble() * maxWidth, random.NextDouble() * maxHeight);
                        gene.points.Add(point1);
                        gene.points.Add(point2);
                        gene.points.Add(point3);
                        break;
                }


                genes.Add(gene);


            }
        }

    }

 
}

public class Gene
{
    public double X { get; set; }
    public double Y { get; set; }
    public double width { get; set; }
    public double height { get; set; }
    public System.Windows.Media.Color color { get; set; }
    public System.Windows.Media.Color backgroundColor {get; set; }
    public Shape shapeToDraw { get; set; }
    public PointCollection points { get; set; } = new PointCollection();

}
