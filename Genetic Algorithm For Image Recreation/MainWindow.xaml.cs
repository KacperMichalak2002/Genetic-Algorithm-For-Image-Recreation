using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Microsoft.Win32;
using System.Diagnostics;
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
        
        // Change to get from user interface
        int numberOfIterations = 100;
        int populationSize = 40;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnToggleRun_Click(object sender, RoutedEventArgs e)
        {
            if (convertedImage == null)
            {
                tbStatus.Text = "Load image first!";
                return;
            }

            if (running)
            {
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    tbStatus.Text = "Stopping";
                }
                running = false;
            }
            else
            {
                running = true;
                btnToggleRun.Content = "Stop";
                tbStatus.Text = "Running";
                ProgressBar.Maximum = numberOfIterations;

                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;

                try
                {
                    

                    sourcePixels = ImageHandler.GetAllPxielsFromBitmap(convertedImage);
                    Draw draw = new Draw(convertedImage.PixelHeight, convertedImage.PixelWidth);

                    var progressHandler = new Progress<(int generationNumber,Individual best)>(report =>
                    {
                        draw.RenderChromosome(report.best);
                        resultImage.Source = draw.CloneCurrentBitmap();
                        tbProgress.Text = $"{report.generationNumber} out of {numberOfIterations}";
                        ProgressBar.Value = report.generationNumber;

                    });

                    GeneticAlgorithm ga = new GeneticAlgorithm(populationSize, numberOfIterations, shapeType, sourcePixels, convertedImage.PixelHeight, convertedImage.PixelWidth);
                    await Task.Run(() => ga.Start(cancellationToken, progressHandler), cancellationToken);

                    tbStatus.Text = "Finished";
                }
                catch (OperationCanceledException)
                {
                    tbStatus.Text = "Stopped by user";
                }
                finally
                {
                    running = false;
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
                image.DecodePixelHeight = 250; //Change to has and given amount of pixels in height
                image.EndInit();

                convertedImage = new FormatConvertedBitmap();
                convertedImage.BeginInit();
                convertedImage.Source = image;
                convertedImage.DestinationFormat = PixelFormats.Bgra32;
                convertedImage.EndInit();


                Debug.WriteLine($"Loaded image height {convertedImage.PixelHeight} Width {convertedImage.PixelWidth}");
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
            shapeType = ShapeType.Triangle;
        }
    }
}