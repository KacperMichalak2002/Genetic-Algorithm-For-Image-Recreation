using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.Model.GA
{
    static class Crossover
    {
        private static void CompleteGenes(List<Gene> childsGenes, Individual parent1, Individual parent2, int minGenes, int maxGenes, Random random)
        {                                                                                                       
            if (parent1.Chromosome.genes.Count == maxGenes)
            {
                for (int i = minGenes; i < maxGenes; i++)
                {
                    if (random.NextDouble() < 0.5)
                        childsGenes.Add(parent1.Chromosome.genes[i].Clone());
                }
            }
            else if (parent2.Chromosome.genes.Count == maxGenes)
            {
                for (int i = minGenes; i < maxGenes; i++)
                {
                    if (random.NextDouble() < 0.5)
                        childsGenes.Add(parent2.Chromosome.genes[i].Clone());
                }
            }
        }


        public static Individual UniformCrossover(Individual parent1, Individual parent2, Random random)
        {
            List<Gene> childsGenes = new List<Gene>();

            int minGenes = Math.Min(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);
            int maxGenes = Math.Max(parent1.Chromosome.genes.Count, parent2.Chromosome.genes.Count);

            for(int i = 0; i < minGenes; i++)
            {
                Gene newGene;

                if(random.NextDouble() < 0.5)
                {
                    newGene = parent1.Chromosome.genes[i].Clone();
                }
                else
                {
                    newGene = parent2.Chromosome.genes[i].Clone();
                }

                childsGenes.Add(newGene);
            }

            CompleteGenes(childsGenes, parent1, parent2, minGenes, maxGenes, random);
            Chromosome childsChromosome = new Chromosome(childsGenes, childsGenes.Count, parent1.Chromosome.imageWidth, parent1.Chromosome.imageHeight, parent1.Chromosome.shapeType);
            Individual child = new Individual(childsChromosome);

            return child;

        }
    }
}
