using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Crossover
    {

        public static Individual BlendCrossover(Individual parent1, Individual parent2)
        {

            List<Gene> childsGenes = new List<Gene>();


            int numberOfGenes = Math.Min(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);


            for (int i = 0; i < numberOfGenes; i++)
            {
                BlendGenes(parent1.Chromosome.genes[i]);
                BlendGenes(parent2.Chromosome.genes[i]);
            }

            childsGenes.AddRange(parent1.Chromosome.genes);
            childsGenes.AddRange(parent2.Chromosome.genes);

            Chromosome childsChromosome = new Chromosome(childsGenes, childsGenes.Count);
            Individual child = new Individual(childsChromosome);

            
            return child;
        }

        private static void BlendGenes (Gene gene)
        {
            byte Alpha = (byte)((int)gene.color.A / 2);
            System.Windows.Media.Color newColor = System.Windows.Media.Color.FromArgb(Alpha, gene.color.R, gene.color.G, gene.color.B);
            gene.color = newColor;
        }

    }
}
