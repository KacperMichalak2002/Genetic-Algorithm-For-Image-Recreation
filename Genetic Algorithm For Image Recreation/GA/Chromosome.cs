using Genetic_Algorithm_For_Image_Recreation.Model.Config;
using Genetic_Algorithm_For_Image_Recreation.Utils;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    
    internal class Chromosome
    {
        public List<Gene> genes {get; set;} = new List<Gene>();
        public int numberOfGenes { get; set;}
        public int imageWidth {get; set;}
        public int imageHeight { get; set; }
        public ShapeType shapeType{get; set;} 

        private static Random randomGene = new Random();


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

        public Chromosome(int numberOfGenes, int imageWidth, int imageHeight, GeneFactoryConfig geneFactoryConfig)
        {
            
            this.numberOfGenes = numberOfGenes;
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.shapeType = shapeType;

            for(int i = 0; i < numberOfGenes; i++)
            {
                GenerateGene(geneFactoryConfig, randomGene);
            }
        }

        public void GenerateGene(GeneFactoryConfig geneFactoryConfig, Random random)
        {
           genes.Add(GeneFactory.GenerateGene(geneFactoryConfig, imageWidth, imageHeight, random));
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

        public void RemoveRandomGene(Random random)
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
