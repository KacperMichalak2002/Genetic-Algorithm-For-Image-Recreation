using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using System.Windows;
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
           Draw draw = new Draw(resultCanva, canvaBorder.ActualHeight, canvaBorder.ActualWidth, new Ellipse());
           draw.StartDrawing();


        }
    }
}