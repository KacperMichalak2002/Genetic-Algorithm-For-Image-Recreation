using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        public int sizeOfPopulation{get; set;}
        public ShapeType shapeType { get; set; }
        public FormatConvertedBitmap convertedBitmap { get; set;}

        private static Random random = new Random();
        private static int numberOfGenes = 5_000;

        private PixelColor[] sourcePixels;
        private double bitmapHeight; 
        private double bitmapWidth;

        public GeneticAlgorithm(int sizeOfPopulation, ShapeType shapeType, PixelColor[] sourcePixels, double bitmapHeight, double bitmapWidth)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.shapeType = shapeType;
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
            this.sourcePixels = sourcePixels;
        }

        private List<Individual> Initialize()
        {
            List<Individual> population = new List<Individual>();

            for (int i = 0; i < sizeOfPopulation; i++)
            {
                population.Add(new Individual
                    (
                        new Chromosome(numberOfGenes, (int)bitmapWidth, (int)bitmapHeight, shapeType)
                    ));
            }

            return population;
        }

        public Individual[] Start()
        {

            List<Individual> population = Initialize();

            int numberOfIterations = 10_000;
            int generation = 0;
            int halfPoint = numberOfIterations / 2;
            Individual[] bestIndividuals = new Individual[3];

            Stopwatch stopwatch = Stopwatch.StartNew();

            for(int genI = 0; genI < numberOfIterations; genI++)
            {

                Parallel.ForEach(population, individual =>
                {
                    CalculateFitnessForPopulation(individual);
                });

                // Sorting by fitness
                population.Sort(new FitnessComparer());

                Debug.WriteLine($"Generation: {generation}");
                Debug.WriteLine($"Best fitness{population[0].fitness}");
                Debug.WriteLine($"Number of genes: {population[0].Chromosome.genes.Count}");

                if (generation == 0)
                {
                    bestIndividuals[0] = population[0];
                }
                else if (generation == halfPoint)
                {
                    bestIndividuals[1] = population[0];
                }
                // Creataing new generation 
                List<Individual> newGeneration = new List<Individual>();


                // Ellitism 10% for testing
                int pct = Math.Max(1, (10 * sizeOfPopulation) / 100);

                for (int i = 0; i < pct; i++)
                {
                    newGeneration.Add(population[i].Clone());
                }

                while(newGeneration.Count < sizeOfPopulation)
                {
                    Individual parent1 = Selection.TournamentSelection(population);
                    Individual parent2 = Selection.TournamentSelection(population);
                    Individual child = Crossover.UniformCrossover(parent1, parent2);

                    if(random.NextDouble() < 0.20) // 20% for mutation
                    {
                        Mutation.Mutate(child, 0.1);
                    }

                    if(random.NextDouble() < 0.02) // 2% for adding new gene
                    {
                        child.Chromosome.GenerateGene(shapeType);
                    }

                    if(random.NextDouble() < 0.01) // 1% for removing random gene
                    {
                        child.Chromosome.RemoveRandomGene();
                    }

                    newGeneration.Add(child);
                }

                population = newGeneration;
                generation++;
            }
            bestIndividuals[2] = population[0];

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Debug.WriteLine($"Time elapsed: {elapsedTime}");

            return bestIndividuals;
        }

        private void CalculateFitnessForPopulation(Individual individual)
        {

            //Pixeles were already calculated individual came from elitism
            if(individual.pixels != null)
            {
                return;
            }

             individual.pixels = PixelRenderer.RenderPixelsToArray(individual);

            if (sourcePixels == null || individual.pixels == null || sourcePixels.Length == 0 || individual.pixels.Length == 0)
                {
                    Debug.WriteLine($"SrcPixels {sourcePixels} L {sourcePixels.Length} \n ResPixels {individual.pixels} L {individual.pixels.Length}");
                }

            individual.fitness = Fitness.CalculateFitness(sourcePixels, individual.pixels);

        }

        private class FitnessComparer : IComparer<Individual>
        {
            public int Compare(Individual? ind1, Individual? ind2)
            {
                return ind1.fitness.CompareTo(ind2?.fitness);
            }
        }
    }
}
