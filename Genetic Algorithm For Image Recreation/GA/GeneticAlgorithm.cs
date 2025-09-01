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
        public Image resultImage { get; set;}
        public FormatConvertedBitmap convertedBitmap { get; set;}
        public Canvas searchVisualSource { get; set;}

        private List<Individual> population;

        private RenderTargetBitmap result;

        public GeneticAlgorithm(int sizeOfPopulation, ShapeType shapeType, Image resultImage, FormatConvertedBitmap convertedBitmap, Canvas searchVisualSource)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.shapeType = shapeType;
            this.resultImage = resultImage;
            this.convertedBitmap = convertedBitmap;
            this.searchVisualSource = searchVisualSource;
        }

        public void Initialize()
        {
            population = new List<Individual>();
            Draw draw = new Draw(convertedBitmap.Height, convertedBitmap.Width);

            searchVisualSource.Children.Clear();

            for (int i = 0; i < sizeOfPopulation; i++)
            {
                population.Add(new Individual
                    (
                        new Chromosome(1, convertedBitmap.Width, convertedBitmap.Height, shapeType)
                    ));
            }


            result = draw.RenderChromosome(population);
            resultImage.Source = result;
        }

        public void Start()
        {

            Initialize();

            Shape? debugShape = null;
           
            foreach (Individual individual in population)
            {
                foreach(Gene gene in individual.Chromosome.genes)
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
                    PixelColor[] resultPixels = ImageHandler.GetPxielFromResultRectangle(result, gene);
                    
                    
                    individual.fitness = Fitness.CalculateFitness(sourcePixels, resultPixels);

                }
            }

            Individual better =  Selection.TournamentSelection(population);

            Debug.WriteLine(better.fitness);

        }
    }
}
