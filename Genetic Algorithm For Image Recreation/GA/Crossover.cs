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

        private static Random random = new Random();

        // Mutation after selection and crossover
        public static Individual BlendCrossover(Individual parent1, Individual parent2)
        {

            List<Gene> childsGenes = new List<Gene>();
            
            int minGenes = Math.Min(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);
            int maxGenes = Math.Max(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);

            for (int i = 0; i < minGenes; i++)
            {
                Gene gene1 = parent1.Chromosome.genes[i];
                Gene gene2 = parent2.Chromosome.genes[i];

                double alpha = random.NextDouble();

                Gene blendedGene = BlendGenes(gene1, gene2, alpha, parent1.Chromosome.imageWidth, parent1.Chromosome.imageHeight);
                childsGenes.Add(blendedGene);
            }

            CompleteGenes(childsGenes, parent1, parent2, minGenes, maxGenes);


            Chromosome childsChromosome = new Chromosome(childsGenes, childsGenes.Count, parent1.Chromosome.imageWidth, parent1.Chromosome.imageHeight, parent1.Chromosome.shapeType);
            Individual child = new Individual(childsChromosome);

            
            return child;
        }

        private static void CompleteGenes(List<Gene> childsGenes, Individual parent1, Individual parent2, int minGenes, int maxGenes)
        {                                                                                                       
            if (parent1.Chromosome.genes.Count == maxGenes)
            {
                for (int i = minGenes; i < maxGenes; i++)
                {
                    if (random.NextDouble() < 0.5)
                        childsGenes.Add(CopyGene(parent1.Chromosome.genes[i]));
                }
            }
            else if (parent2.Chromosome.genes.Count == maxGenes)
            {
                for (int i = minGenes; i < maxGenes; i++)
                {
                    if (random.NextDouble() < 0.5)
                        childsGenes.Add(CopyGene(parent2.Chromosome.genes[i]));
                }
            }
        }


        public static Individual UniformCrossover(Individual parent1, Individual parent2)
        {
            List<Gene> childsGenes = new List<Gene>();

            int minGenes = Math.Min(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);
            int maxGenes = Math.Max(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);

            for(int i = 0; i < minGenes; i++)
            {
                Gene newGene;

                if(random.NextDouble() < 0.5)
                {
                    newGene = CopyGene(parent1.Chromosome.genes[i]);
                }
                else
                {
                    newGene = CopyGene(parent2.Chromosome.genes[i]);
                }

                childsGenes.Add(newGene);
            }

            CompleteGenes(childsGenes, parent1, parent2, minGenes, maxGenes);
            Chromosome childsChromosome = new Chromosome(childsGenes, childsGenes.Count, parent1.Chromosome.imageWidth, parent1.Chromosome.imageHeight, parent1.Chromosome.shapeType);
            Individual child = new Individual(childsChromosome);

            return child;

        }

        private static Gene BlendGenes (Gene gene1 , Gene gene2, double alpha, int imageWidth, int imageHeight)
        {
            

            int X = (int)(gene1.X * alpha + gene2.X * (1 - alpha));
            int Y = (int)(gene1.Y * alpha + gene2.Y * (1 - alpha));
            int width = (int)(gene1.width * alpha + gene2.width * (1 - alpha));
            int height = (int)(gene1.height * alpha + gene2.height * (1 - alpha));

            if (X + width > imageWidth)
            {
                width = imageWidth - X;
            }

            if (Y + height > imageHeight)
            {
                height = imageHeight - Y;
            }

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

            return newGene;
        }

        private static Gene CopyGene(Gene gene)
        {
            Gene coppiedGene = new Gene
            {
                X = gene.X,
                Y = gene.Y,
                width = gene.width,
                height = gene.height,
                color = gene.color,
                ShapeType = gene.ShapeType
            };

            return coppiedGene;
        }
    }
}
