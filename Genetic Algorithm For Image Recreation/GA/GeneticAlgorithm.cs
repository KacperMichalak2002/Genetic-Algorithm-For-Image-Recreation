using Genetic_Algorithm_For_Image_Recreation.Renderer;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        public int sizeOfPopulation{get; set;}
        public Border canvaBorder {get; set;}
        public ShapeType shapeType { get; set; }
        public Image resultImage { get; set;}
        public FormatConvertedBitmap convertedBitmap { get; set;}
        public Canvas searchVisualSource { get; set;}

        public GeneticAlgorithm(int sizeOfPopulation, Border canvaBorder, ShapeType shapeType, Image resultImage, FormatConvertedBitmap convertedBitmap, Canvas searchVisualSource)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.canvaBorder = canvaBorder;
            this.shapeType = shapeType;
            this.resultImage = resultImage;
            this.convertedBitmap = convertedBitmap;
            this.searchVisualSource = searchVisualSource;
        }

        public void Start()
        {
            List<Chromosome> population = new List<Chromosome>();
            Draw draw = new Draw(canvaBorder.ActualHeight, canvaBorder.ActualWidth);

            searchVisualSource.Children.Clear();

            for (int i = 0; i < sizeOfPopulation; i++)
            {
                population.Add(new Chromosome(3, canvaBorder.ActualWidth, canvaBorder.ActualHeight, shapeType));
            }

            resultImage.Source = draw.RenderChromosome(population);

            foreach(Chromosome chromosome in population)
            {
                foreach(Gene gene in chromosome.genes)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Height = gene.height;
                    rectangle.Width = gene.width;
                    rectangle.Stroke = System.Windows.Media.Brushes.Black;

                    Canvas.SetLeft(rectangle, gene.X);
                    Canvas.SetTop(rectangle, gene.Y);

                    searchVisualSource.Children.Add(rectangle);
                }
            }
        }
    }
}
