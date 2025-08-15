using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           Draw draw = new Draw(resultCanva, canvaBorder.ActualHeight, canvaBorder.ActualWidth, ShapeType.Ellipse);
           draw.StartDrawing();


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
                //image.DecodePixelHeight = 50; Change to has and given amount of pixels in height
                image.EndInit();

                txtBlock1.Text = ImageHandler.Test(image);


                srcImage.Source = image;

            }
        }
    }
}