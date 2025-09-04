using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    
    internal class Chromosome
    {
        public List<Gene> genes {get; set;} = new List<Gene>();
        public int numberOfGenes { get; set;}

        private static Random random = new Random();


        public Chromosome()
        {

        }

        public Chromosome(List<Gene> genes, int numberOfGenes)
        {
            this.genes = genes;
            this.numberOfGenes = numberOfGenes;
        }

        public Chromosome(int numberOfGenes, double maxWidth, double maxHeight, ShapeType shapeType)
        {
            
            this.numberOfGenes = numberOfGenes;

            for(int i = 0; i < numberOfGenes; i++)
            {
                Gene gene = new Gene
                {
                    ShapeType = shapeType,
                    X = random.NextDouble() * maxWidth,
                    Y = random.NextDouble() * maxHeight,
                    color = System.Windows.Media.Color.FromRgb(255,0,0),
                    //color = System.Windows.Media.Color.FromRgb((byte) random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    backgroundColor = System.Windows.Media.Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                };

                switch (shapeType)
                {
                    case ShapeType.Ellipse:
                    case ShapeType.Rectangle:
                        double maxPossibleWidth = maxWidth - gene.X;
                        double maxPossibleHeight = maxHeight - gene.Y;
                        gene.width = random.NextDouble() * maxPossibleWidth;
                        gene.height = random.NextDouble() * maxPossibleHeight;
                        break;
                    case ShapeType.Triangle:
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

public enum ShapeType
{
    Ellipse,
    Rectangle,
    Triangle
}

public class Gene
{
    public double X { get; set; }
    public double Y { get; set; }
    public double width { get; set; }
    public double height { get; set; }
    public System.Windows.Media.Color color { get; set; }
    public System.Windows.Media.Color backgroundColor {get; set; }
    public ShapeType ShapeType { get; set; }
    public PointCollection points { get; set; } = new PointCollection();

}
