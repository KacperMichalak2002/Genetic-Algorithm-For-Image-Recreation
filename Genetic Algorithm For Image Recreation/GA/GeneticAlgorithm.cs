using Genetic_Algorithm_For_Image_Recreation.Renderer;
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

        public GeneticAlgorithm(int sizeOfPopulation, ShapeType shapeType, Image resultImage, FormatConvertedBitmap convertedBitmap, Canvas searchVisualSource)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.shapeType = shapeType;
            this.resultImage = resultImage;
            this.convertedBitmap = convertedBitmap;
            this.searchVisualSource = searchVisualSource;
        }

        public void Start()
        {
            List<Chromosome> population = new List<Chromosome>();
            Draw draw = new Draw(convertedBitmap.Height, convertedBitmap.Width);

            searchVisualSource.Children.Clear();

            for (int i = 0; i < sizeOfPopulation; i++)
            {
                population.Add(new Chromosome(1, convertedBitmap.Width, convertedBitmap.Height, shapeType));
            }


            RenderTargetBitmap result = draw.RenderChromosome(population);

            resultImage.Source = result;


            Shape debugShape = null;
            // For debug puropse
            foreach (Chromosome chromosome in population)
            {
                foreach(Gene gene in chromosome.genes)
                {
                    
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
                    //ImageHandler.GetPixelFromSourceRectangle(convertedBitmap, gene);
                    ImageHandler.GetPxielFromResultRectangle(result, gene);
                    
                }
            }
        }
    }
}
