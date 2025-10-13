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
                        new Chromosome(4, convertedBitmap.Width, convertedBitmap.Height, shapeType)
                    ));
            }
        }

        public void Start()
        {

            Initialize();

           
            int numberOfIterations = 20;
            int generation = 0;

            

            for(int genI = 0; genI < numberOfIterations; genI++)
            {

                List<RenderTargetBitmap> renderTargetBitmaps = RenderPopulation();

                CalculateFitnessForPopulation(renderTargetBitmaps);
                
                // Sorting by fitness
                population.Sort(new FitnessComparer());

                Debug.WriteLine($"Generation: {generation}");
                Debug.WriteLine($"Best fitness{population[0].fitness}");

                resultImages[0].Source = draw.RenderChromosome(population[2]);
                resultImages[1].Source = draw.RenderChromosome(population[1]);
                resultImages[2].Source = draw.RenderChromosome(population[0]);

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

                    newGeneration.Add(child);
                }

                population = newGeneration;
                generation++;
            }
        }

        private List<RenderTargetBitmap> RenderPopulation()
        {
            List<RenderTargetBitmap> bitmaps = new List<RenderTargetBitmap>();

            foreach(Individual individual in population)
            {
                RenderTargetBitmap bitmap = draw.RenderChromosome(individual);
                bitmaps.Add(bitmap);
            }

            return bitmaps;
        }

        private void CalculateFitnessForPopulation(List<RenderTargetBitmap> renderTargetBitmaps)
        {

            for(int i = 0; i < population.Count; i++)
            {
                Individual individual = population[i];
                double individualFitness = 0;


                foreach (Gene gene in individual.Chromosome.genes)
                {

                    PixelColor[] sourcePixels = ImageHandler.GetPixelFromSourceRectangle(convertedBitmap, gene);
                    PixelColor[] resultPixels = ImageHandler.GetPxielFromResultRectangle(renderTargetBitmaps[i], gene);


                    individualFitness += Fitness.CalculateFitness(sourcePixels, resultPixels);
                    if (sourcePixels == null || resultPixels == null || sourcePixels.Length == 0 || resultPixels.Length == 0)
                    {
                        Debug.WriteLine($"SrcPixels {sourcePixels} L {sourcePixels.Length} \n ResPixels {resultPixels} L {resultPixels.Length}");
                    }

                }
                individual.fitness = individualFitness / individual.Chromosome.genes.Count;
            }
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
