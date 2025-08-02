using Genetic_Algorithm_For_Image_Recreation.GA;
using System.Windows;

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
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
            Chromosome chromosome = new Chromosome();
            txtBlock1.Text = chromosome.value;
        }
    }
}