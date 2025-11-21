using Genetic_Algorithm_For_Image_Recreation.Utils;
using System;
using System.Diagnostics;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        private AlgorithmConfig algorithmConfig;

        public GeneticAlgorithm(AlgorithmConfig algorithmConfig)
        {
            this.algorithmConfig = algorithmConfig;
        }

        private Individual[] Initialize()
        {
            Individual[] population = new Individual[algorithmConfig.sizeOfPopulation];

            for (int i = 0; i < algorithmConfig.sizeOfPopulation; i++)
            {
                population[i] = (new Individual
                    (
                        new Chromosome(
                            algorithmConfig.numberOfGenes,
                            algorithmConfig.bitmapWidth,
                            algorithmConfig.bitmapHeight,
                            algorithmConfig.maxGeneScale,
                            algorithmConfig.shapeType
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

                    if (random.NextDouble() < 0.20) // 20% for mutation
                    {
                        Mutation.Mutate(child, 0.1, algorithmConfig.maxGeneScale, random);
                    }

                    if (random.NextDouble() < 0.02) // 2% for adding new gene
                    {
                        child.Chromosome.GenerateGene(algorithmConfig.shapeType, algorithmConfig.maxGeneScale, random);
                    }

                    if (random.NextDouble() < 0.01) // 1% for removing random gene
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


        }

        private void CalculateFitnessForPopulation(Individual individual)
        {

            //Pixeles were already calculated individual came from elitism
            if (individual.pixels != null)
            {
                return;
            }

            individual.pixels = PixelRenderer.RenderPixelsToArray(individual);

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