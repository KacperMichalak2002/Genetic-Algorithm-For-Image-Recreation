using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation
{
    public partial class MainWindow : Window
    {
        private FormatConvertedBitmap convertedImage;
        private ShapeType shapeType;
        private PixelColor[] sourcePixels;
        private bool running = false;
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnToggleRun_Click(object sender, RoutedEventArgs e)
        {
            if (convertedImage == null)
            {
                txtBlock1.Text = "Load image first!";
                return;
            }

            if (running)
            {
                if(cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    txtBlock1.Text = "Stopping";
                }
                running = false;
            }
            else
            {
                running = true;
                btnToggleRun.Content = "Stop";
                txtBlock1.Text = "Running";

                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                try
                {
                    int numberOfIterations = 1000;
                    int populationSize = 40;

                    sourcePixels = ImageHandler.GetAllPxielsFromBitmap(convertedImage);
                    Draw draw = new Draw(convertedImage.Height, convertedImage.Width);

                    var progressHandler = new Progress<Individual>(bestIndividual =>
                    {
                        draw.RenderChromosome(bestIndividual);
                        resultImage.Source = draw.CloneCurrentBitmap();
                    });
                    
                    GeneticAlgorithm ga = new GeneticAlgorithm(populationSize, numberOfIterations ,shapeType, sourcePixels, convertedImage.Height, convertedImage.Width);
                    await Task.Run(() => ga.Start(cancellationToken, progressHandler), cancellationToken);
                    
                    txtBlock1.Text = "Finished";
                }
                catch(OperationCanceledException)
                {
                    txtBlock1.Text = "Stopped by user";
                }
                finally
                {
                    running = true;
                    btnToggleRun.Content = "Start";
                }
            }

        }

        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileBrowser = new OpenFileDialog();
            fileBrowser.Filter = "Image Files| *.jpg;*.png";

            Boolean? result = fileBrowser.ShowDialog();

            if (result.Equals(true))
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.UriSource = new Uri(fileBrowser.FileName);
                image.CacheOption = BitmapCacheOption.OnLoad;
                //image.DecodePixelHeight = 50; Change to has and given amount of pixels in height
                image.EndInit();



                convertedImage = new FormatConvertedBitmap();
                convertedImage.BeginInit();
                convertedImage.Source = image;
                convertedImage.DestinationFormat = PixelFormats.Bgra32;
                convertedImage.EndInit();

                //ImageHandler.RectangleScanningSource(convertedImage);

                srcImage.Source = convertedImage;

            }
        }

        private void rbEllipse_Checked(object sender, RoutedEventArgs e)
        {
            shapeType = ShapeType.Ellipse;
        }

        private void rbRectangle_Checked(object sender, RoutedEventArgs e)
        {
            shapeType = ShapeType.Rectangle;
        }

        private void rbTriangle_Checked(object sender, RoutedEventArgs e)
        {
            shapeType= ShapeType.Triangle;
        }
    }
}