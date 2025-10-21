using Genetic_Algorithm_For_Image_Recreation.Renderer;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        public int sizeOfPopulation{get; set;}
        public ShapeType shapeType { get; set; }
        public List<Image> resultImages { get; set;}
        public FormatConvertedBitmap convertedBitmap { get; set;}
        public Canvas searchVisualSource { get; set;}

        private List<Individual> population;
        private static Random random = new Random();
        private static int numberOfGenes = 100;

        Draw draw;

        private RenderTargetBitmap result1;
        private RenderTargetBitmap result2;
        private RenderTargetBitmap result3;

        public GeneticAlgorithm(int sizeOfPopulation, ShapeType shapeType, List<Image> resultImages, FormatConvertedBitmap convertedBitmap, Canvas searchVisualSource)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.shapeType = shapeType;
            this.resultImages = resultImages;
            this.convertedBitmap = convertedBitmap;
            this.searchVisualSource = searchVisualSource;
        }

        public void Initialize()
        {
            population = new List<Individual>();

            draw = new Draw(convertedBitmap.Height, convertedBitmap.Width);

            searchVisualSource.Children.Clear();

            for (int i = 0; i < sizeOfPopulation; i++)
            {
                population.Add(new Individual
                    (
                        new Chromosome(numberOfGenes, (int)convertedBitmap.Width, (int)convertedBitmap.Height, shapeType)
                    ));
            }
        }

        public void Start()
        {

            Initialize();

           
            int numberOfIterations = 200;
            int generation = 0;
            int halfPoint = numberOfIterations / 2;
            

            for(int genI = 0; genI < numberOfIterations; genI++)
            {

                foreach (Individual individual in population)
                {
                    RenderTargetBitmap bitmap = draw.RenderChromosome(individual);

                    CalculateFitnessForPopulation(individual ,bitmap);
                    bitmap?.Clear();
                }
                // Sorting by fitness
                population.Sort(new FitnessComparer());

                Debug.WriteLine($"Generation: {generation}");
                Debug.WriteLine($"Best fitness{population[0].fitness}");
                Debug.WriteLine($"Number of genes: {population[0].Chromosome.genes.Count}");


                if (generation == 0)
                {
                    draw.RenderChromosome(population[0]);
                    resultImages[0].Source = draw.CloneCurrentBitmap();
                }else if (generation == halfPoint)
                {
                    draw.RenderChromosome(population[0]);
                    resultImages[1].Source = draw.CloneCurrentBitmap();
                }
                // Creataing new generation 
                List<Individual> newGeneration = new List<Individual>();


                // Ellitism 10% for testing
                int pct = Math.Max(1, (10 * sizeOfPopulation) / 100);

                for (int i = 0; i < pct; i++)
                {
                    newGeneration.Add(population[i]);
                }

                while(newGeneration.Count < sizeOfPopulation)
                {
                    Individual parent1 = Selection.TournamentSelection(population);
                    Individual parent2 = Selection.TournamentSelection(population);
                    Individual child = Crossover.BlendCrossover(parent1, parent2);

                    if(random.NextDouble() < 0.2) // 20% for mutation
                    {
                        Mutation.Mutate(child, 0.1);
                    }

                    if(random.NextDouble() < 0.1 && child.Chromosome.genes.Count < numberOfGenes + 100)
                    {
                        child.Chromosome.GenerateGene(shapeType);
                    }

                    newGeneration.Add(child);
                }

                population = newGeneration;
                generation++;
            }
            draw.RenderChromosome(population[0]);
            resultImages[2].Source = draw.CloneCurrentBitmap();
        }

        private void CalculateFitnessForPopulation(Individual individual, RenderTargetBitmap individualBitmap)
        {
            double individualFitness = 0;
            foreach (Gene gene in individual.Chromosome.genes)
            {
            
                PixelColor[] sourcePixels = ImageHandler.GetPixelFromSourceRectangle(convertedBitmap, gene);
                PixelColor[] resultPixels = ImageHandler.GetPxielFromResultRectangle(individualBitmap, gene);

                if (sourcePixels == null || resultPixels == null || sourcePixels.Length == 0 || resultPixels.Length == 0)
                {
                    Debug.WriteLine($"SrcPixels {sourcePixels} L {sourcePixels.Length} \n ResPixels {resultPixels} L {resultPixels.Length}");
                }

                individualFitness += Fitness.CalculateFitness(sourcePixels, resultPixels);

            }
            individual.fitness = individualFitness / individual.Chromosome.genes.Count;

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
