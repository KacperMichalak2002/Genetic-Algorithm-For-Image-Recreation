using System.Windows.Media;

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

            int geneX = random.Next(0, imageWidth - geneWidth);
            int geneY = random.Next(0, imageHeight - geneHeight);


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
                    //double maxPossibleWidth = maxWidth - gene.X;
                    //double maxPossibleHeight = maxHeight - gene.Y;
                    //gene.width = random.Next(1, maxWidth + 1);
                    //gene.height = random.Next(1, maxHeight + 1);

                    //if(gene.X + gene.width > imageWidth)
                    //{
                    //    gene.width = imageWidth - gene.X;
                    //}
                    //if(gene.Y + gene.height > imageHeight)
                    //{
                    //    gene.height = imageHeight - gene.Y;
                    //}

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
    public PointCollection points { get; set; } = new PointCollection();

    public Gene Clone()
    {
        Gene coppiedGene = new Gene
        {
            X = this.X,
            Y = this.Y,
            width = this.width,
            height = this.height,
            color = this.color,
            ShapeType = this.ShapeType
        };

        return coppiedGene;
    }

}
