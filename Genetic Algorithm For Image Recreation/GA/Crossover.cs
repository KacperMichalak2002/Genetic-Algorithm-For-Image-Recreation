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

        // Mutation after selection and crossover
        public static Individual BlendCrossover(Individual parent1, Individual parent2)
        {

            List<Gene> childsGenes = new List<Gene>();
            Random random = new Random();

            int numberOfGenes = Math.Min(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);


            for (int i = 0; i < numberOfGenes; i++)
            {
                Gene gene1 = parent1.Chromosome.genes[i];
                Gene gene2 = parent2.Chromosome.genes[i];

                double alpha = random.NextDouble();

                double X = gene1.X * alpha + gene2.X * (1 - alpha);
                double Y = gene1.Y * alpha + gene2.Y * (1 - alpha);
                double width = gene1.width * alpha + gene2.width * (1 - alpha);
                double height = gene1.height * alpha + gene2.height * (1 - alpha);

                byte r = (byte)(gene1.color.R * alpha + gene2.color.R * (1 - alpha));
                byte g = (byte)(gene1.color.G * alpha + gene2.color.G * (1 - alpha));
                byte b = (byte)(gene1.color.B * alpha + gene2.color.B * (1 - alpha));
                byte a = (byte)(gene1.color.A * alpha + gene2.color.A * (1 - alpha));

                Color newColor = Color.FromArgb(a, r, g, b);

                Gene newGene = new Gene
                {
                    X = X,
                    Y = Y,
                    width = width,
                    height = height,
                    color = newColor,
                    ShapeType = gene1.ShapeType
                };

                childsGenes.Add(newGene);

            }

            Chromosome childsChromosome = new Chromosome(childsGenes, childsGenes.Count, parent1.Chromosome.maxWidth, parent1.Chromosome.maxHeight, parent1.Chromosome.shapeType);
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
