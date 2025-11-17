namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    
    internal class Chromosome
    {
        public List<Gene> genes {get; set;} = new List<Gene>();
        public int numberOfGenes { get; set;}
        public int imageWidth {get; set;}
        public int imageHeight { get; set; }
        public ShapeType shapeType{get; set;} 

        private static Random random = new Random();


        public Chromosome()
        {

        }

        public Chromosome(List<Gene> genes, int numberOfGenes, int imageWidth, int imageHeight, ShapeType shapeType)
        {
            this.genes = genes;
            this.numberOfGenes = numberOfGenes;
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.shapeType = shapeType;
        }

        public Chromosome(int numberOfGenes, int imageWidth, int imageHeight, ShapeType shapeType)
        {
            
            this.numberOfGenes = numberOfGenes;
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.shapeType = shapeType;

            for(int i = 0; i < numberOfGenes; i++)
            {
                GenerateGene(shapeType);
            }
        }

        public void GenerateGene(ShapeType shapeType)
        {
            double procentOfImage = 0.10;
            int maxWidth = (int)(imageWidth * procentOfImage);
            int maxHeight = (int)(imageHeight * procentOfImage);

            int geneWidth = random.Next(1, maxWidth + 1);
            int geneHeight = random.Next(1, maxHeight + 1);

            int maxValueOfX = imageWidth - geneWidth;
            int maxValueOfY = imageHeight - geneHeight;

            int geneX = random.Next(0, maxValueOfX);
            int geneY = random.Next(0, maxValueOfY);


            Gene gene = new Gene
            {
                ShapeType = shapeType,
                X = geneX,
                Y = geneY,
                width = geneWidth,
                height = geneHeight,
                //color = System.Windows.Media.Color.FromRgb(255, 0, 0),
                color = System.Windows.Media.Color.FromArgb(
                    (byte)random.Next(100,200),
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


            genes.Add(gene);
        }


        public Chromosome Clone()
        {
            List<Gene> clonedGenes = this.genes.Select(genes => genes.Clone()).ToList();
            Chromosome newChromosome = new Chromosome
            {
                genes = clonedGenes,
                numberOfGenes = this.numberOfGenes,
                imageHeight = this.imageHeight,
                imageWidth = this.imageWidth,
                shapeType = this.shapeType
            };

            return newChromosome;
        }

        public void RemoveRandomGene()
        {
            if(genes.Count > 500)
            {
                int indexToRemove = random.Next(0, genes.Count);
                genes.RemoveAt(indexToRemove);
            }
        }
    }

 
}

public struct BasicPoint
{
    public int X { get; }
    public int Y  { get; }

    public BasicPoint(int x, int y)
    {
        X = x;
        Y = y;
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
    public int X { get; set; }
    public int Y { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public System.Windows.Media.Color color { get; set; }
    public System.Windows.Media.Color backgroundColor {get; set; }
    public ShapeType ShapeType { get; set; }
    public List<BasicPoint> points { get; set; } = new List<BasicPoint>();

    public Gene Clone()
    {
        Gene coppiedGene = new Gene
        {
            X = this.X,
            Y = this.Y,
            width = this.width,
            height = this.height,
            color = this.color,
            ShapeType = this.ShapeType,
            points = new List<BasicPoint>(this.points)
        };

        return coppiedGene;
    }

}
