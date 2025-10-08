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

            int i = 0;

            Shape? debugShape = null;

            
            foreach (Individual individual in population)
            {
                double individualFitness = 0;
                foreach (Gene gene in individual.Chromosome.genes)
                {
                    // For debug puropse
                    switch (shapeType)
                    {
                        case ShapeType.Rectangle:
                            debugShape = new Rectangle
                            {
                                Height = gene.height,
                                Width = gene.width,
                                Stroke = Brushes.Black
                            };
                            Canvas.SetLeft(debugShape, gene.X);
                            Canvas.SetTop(debugShape, gene.Y);
                            break;
                        case ShapeType.Ellipse:
                            debugShape = new Ellipse
                            {
                                Width = gene.width,
                                Height = gene.height,
                                Stroke = Brushes.Black
                            };
                            Canvas.SetLeft(debugShape, gene.X);
                            Canvas.SetTop(debugShape, gene.Y);
                            break;
                        case ShapeType.Triangle:
                            Polygon pol = new Polygon
                            {
                                Points = new PointCollection(gene.points),
                                Stroke = Brushes.Black
                            };

                            debugShape = pol;
                            break;

                    }
                    searchVisualSource.Children.Add(debugShape);


                    PixelColor[] sourcePixels = ImageHandler.GetPixelFromSourceRectangle(convertedBitmap, gene);
                    PixelColor[] resultPixels = ImageHandler.GetPxielFromResultRectangle(renderTargetBitmaps[i], gene);


                    individualFitness  += Fitness.CalculateFitness(sourcePixels, resultPixels);

                }
                individual.fitness = individualFitness / individual.Chromosome.genes.Count;
                i++;
            }

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
}
