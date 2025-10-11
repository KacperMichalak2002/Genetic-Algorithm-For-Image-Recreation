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

        Draw draw;


        List<RenderTargetBitmap> renderTargetBitmaps = new List<RenderTargetBitmap>();
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
                        new Chromosome(2, convertedBitmap.Width, convertedBitmap.Height, shapeType)
                    ));

                renderTargetBitmaps.Add(draw.RenderChromosome(population[i]));
            }
        }

        public void Start()
        {

            Initialize();

           
            int numberOfIterations = 20;

            

            for(int genI = 0; genI < numberOfIterations; genI++)
            {
                int i = 0;
                foreach (Individual individual in population)
                {
                    double individualFitness = 0;
                    foreach (Gene gene in individual.Chromosome.genes)
                    {

                        PixelColor[] sourcePixels = ImageHandler.GetPixelFromSourceRectangle(convertedBitmap, gene);
                        PixelColor[] resultPixels = ImageHandler.GetPxielFromResultRectangle(renderTargetBitmaps[i], gene);


                        individualFitness += Fitness.CalculateFitness(sourcePixels, resultPixels);
                        if(sourcePixels == null || resultPixels == null || sourcePixels.Length == 0 || resultPixels.Length == 0)
                        {
                            Debug.WriteLine($"SrcPixels {sourcePixels} L {sourcePixels.Length} \n ResPixels {resultPixels} L {resultPixels.Length}");
                        }

                    }
                    individual.fitness = individualFitness / individual.Chromosome.genes.Count;
                    i++;
                }
                

                // Sorting by fitness
                population.Sort(new FitnessComparer());

                // Ellitism


                // Some % of the population for crossover

                Individual parent1 = Selection.TournamentSelection(population);
                Individual parent2 = Selection.TournamentSelection(population);


                Individual child = Crossover.BlendCrossover(parent1, parent2);

                child.Chromosome.Mutate();

                result3 = draw.RenderChromosome(child);

                resultImages[0].Source = draw.RenderChromosome(parent1);
                resultImages[1].Source = draw.RenderChromosome(parent2);
                resultImages[2].Source = result3;
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
