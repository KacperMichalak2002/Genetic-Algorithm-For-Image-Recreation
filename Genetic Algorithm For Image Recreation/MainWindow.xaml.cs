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
        private ShapeType shapeType;
        private PixelColor[] sourcePixels;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(convertedImage == null)
            {
                txtBlock1.Text = "Load image first!";
                return;
            }

            // List of images you want to be drawn
            List<Image> resultImages = new List<Image>
            {
                resultImage,
                resultImage2,
                resultImage3
            };

            sourcePixels = ImageHandler.GetAllPxielsFromBitmap(convertedImage);
            GeneticAlgorithm ga = new GeneticAlgorithm(100, shapeType, sourcePixels, convertedImage.Height, convertedImage.Width);
            Individual[] individualsToDraw = await Task.Run(() => ga.Start());
            Draw draw = new Draw(convertedImage.Height, convertedImage.Width);

            for(int i = 0; i < individualsToDraw.Length; i++)
            {
                draw.RenderChromosome(individualsToDraw[i]);
                resultImages[i].Source = draw.CloneCurrentBitmap();
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