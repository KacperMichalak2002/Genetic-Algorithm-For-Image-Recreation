using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation
{
    public partial class MainWindow : Window
    {
        private FormatConvertedBitmap convertedImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(convertedImage == null)
            {
                txtBlock1.Text = "Load image first!";
                return;
            }

            GeneticAlgorithm ga = new GeneticAlgorithm(1, canvaBorder, ShapeType.Rectangle, resultImage, convertedImage, searchVisualSource);
            ga.Start();
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
                convertedImage.DestinationFormat = PixelFormats.Pbgra32;
                convertedImage.EndInit();

                ImageHandler.RectangleScanningSource(convertedImage);

                srcImage.Source = convertedImage;

            }
        }
    }
}