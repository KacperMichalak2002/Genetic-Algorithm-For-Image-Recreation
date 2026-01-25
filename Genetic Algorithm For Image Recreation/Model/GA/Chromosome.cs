using Genetic_Algorithm_For_Image_Recreation.Model.Config;
using Genetic_Algorithm_For_Image_Recreation.Model.Factories;

namespace Genetic_Algorithm_For_Image_Recreation.Model.GA
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
            shapeType = shapeType;

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
            List<Gene> clonedGenes = genes.Select(genes => genes.Clone()).ToList();
            Chromosome newChromosome = new Chromosome
            {
                genes = clonedGenes,
                numberOfGenes = numberOfGenes,
                imageHeight = imageHeight,
                imageWidth = imageWidth,
                shapeType = shapeType
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
    public ShapeType ShapeType { get; set; }
    public List<BasicPoint> points { get; set; } = new List<BasicPoint>();

    public Gene Clone()
    {
        Gene coppiedGene = new Gene
        {
            X = X,
            Y = Y,
            width = width,
            height = height,
            color = color,
            ShapeType = ShapeType,
            points = new List<BasicPoint>(points)
        };

        return coppiedGene;
    }

}
