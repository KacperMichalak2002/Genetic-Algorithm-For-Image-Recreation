using Genetic_Algorithm_For_Image_Recreation.Model.Config;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using System.Diagnostics;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        private AlgorithmConfig algorithmConfig;
        private GeneFactoryConfig geneFactoryConfig;

        public GeneticAlgorithm(AlgorithmConfig algorithmConfig)
        {
            this.algorithmConfig = algorithmConfig;
            this.geneFactoryConfig = new GeneFactoryConfig
                (
                    algorithmConfig.geneConfig,
                    algorithmConfig.bitmapWidth,
                    algorithmConfig.bitmapHeight
                );
        }

        private Individual[] Initialize()
        {
            Individual[] population = new Individual[algorithmConfig.sizeOfPopulation];

            for (int i = 0; i < algorithmConfig.sizeOfPopulation; i++)
            {
                population[i] = (new Individual
                    (
                        new Chromosome(
                            algorithmConfig.geneConfig.numberOfGenes,
                            algorithmConfig.bitmapWidth,
                            algorithmConfig.bitmapHeight,
                            geneFactoryConfig

                            )
                    ));
            }

            return population;
        }

        public void Start(CancellationToken cancellationToken, IProgress<(int generationNumber, Individual best)> progress)
        {

            Individual[] population = Initialize();

            var updateTimer = Stopwatch.StartNew();
            long updateInterval = 100;

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int generation = 1; generation <= algorithmConfig.numberOfIterations; generation++)
            {

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }


                Parallel.ForEach(population, individual =>
                {
                    CalculateFitnessForPopulation(individual);
                });

                // Sorting by fitness
                Array.Sort(population, new FitnessComparer());


                if(generation % 10 == 0)
                {
                    Debug.WriteLine($"Generation: {generation}");
                    Debug.WriteLine($"Best fitness: {population[0].fitness}");
                    Debug.WriteLine($"Number of genes: {population[0].Chromosome.genes.Count}");
                }
                

                if (updateTimer.ElapsedMilliseconds > updateInterval)
                {
                    progress.Report((generation, population[0]));
                    updateTimer.Restart();
                }

                // Creataing new generation 
                Individual[] newGeneration = new Individual[algorithmConfig.sizeOfPopulation];


                // Ellitism 10% for testing
                int pct = Math.Max(1, (10 * algorithmConfig.sizeOfPopulation) / 100);

                for (int i = 0; i < pct; i++)
                {
                    newGeneration[i] = population[i].Clone();
                }

                Parallel.For(pct, algorithmConfig.sizeOfPopulation, i =>
                {
                    Random random = Random.Shared;

                    Individual parent1 = Selection.TournamentSelection(population, random);
                    Individual parent2 = Selection.TournamentSelection(population, random);
                    Individual child = Crossover.UniformCrossover(parent1, parent2, random);

                    if (random.NextDouble() < algorithmConfig.mutationRate)
                    {
                        Mutation.Mutate(child,algorithmConfig, random);
                    }

                    if (random.NextDouble() < 0.02)
                    {
                        child.Chromosome.GenerateGene(geneFactoryConfig, random);
                    }

                    if (random.NextDouble() < 0.01)
                    {
                        child.Chromosome.RemoveRandomGene(random);
                    }

                    newGeneration[i] = child;
                });


                population = newGeneration;
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Debug.WriteLine($"\nTime elapsed: {elapsedTime}");

            Debug.WriteLine($"Best fitness: {population[0].fitness}");
            Debug.WriteLine($"Size of population: {algorithmConfig.sizeOfPopulation}");
            Debug.WriteLine($"Number of genes: {population[0].Chromosome.genes.Count}");
            Debug.WriteLine($"Alpha values: MIN: {algorithmConfig.geneConfig.minAlpha} MAX {algorithmConfig.geneConfig.maxAlpha}");


        }

        private void CalculateFitnessForPopulation(Individual individual)
        {

            //Pixeles were already calculated individual came from elitism
            if (individual.pixels != null)
            {
                return;
            }

            individual.pixels = PixelRenderer.RenderPixelsToArray(individual, algorithmConfig.sourcePixels[0]);

            individual.fitness = Fitness.CalculateFitness(algorithmConfig.sourcePixels, individual.pixels, individual.Chromosome.genes.Count);

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