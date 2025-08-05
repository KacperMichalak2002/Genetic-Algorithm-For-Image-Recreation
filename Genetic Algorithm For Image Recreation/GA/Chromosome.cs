using System.Drawing;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    
    internal class Chromosome
    {
        public List<Gene> genes {get; set;} = new List<Gene>();

        public Chromosome(int numberOfGenes, double maxWidth, double maxHeight)
        {
            Random random = new Random();

            for(int i = 0; i < numberOfGenes; i++)
            {
                genes.Add(new Gene
                {
                    X = random.NextDouble() * maxWidth,
                    Y = random.NextDouble() * maxHeight,
                    width = random.NextDouble() * maxWidth,
                    height = random.NextDouble() * maxHeight,
                    color = System.Windows.Media.Color.FromRgb((byte) random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    backgroundColor = System.Windows.Media.Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                });
            }
        }

    }

 
}
public class Gene
{
    public double X, Y;
    public double width, height;
    public System.Windows.Media.Color color, backgroundColor;

}
