using ControlzEx.Theming;
using Genetic_Algorithm_For_Image_Recreation.ViewModel;
using System.Windows;

namespace Genetic_Algorithm_For_Image_Recreation
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            DataContext = viewModel;

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }

        private void rbEllipse_Checked(object sender, RoutedEventArgs e)
        {
            //shapeType = ShapeType.Ellipse;
        }

        private void rbRectangle_Checked(object sender, RoutedEventArgs e)
        {
            //shapeType = ShapeType.Rectangle;
        }

        private void rbTriangle_Checked(object sender, RoutedEventArgs e)
        {
           // shapeType = ShapeType.Triangle;
        }

        private void MetroProgressBar_DropDownOpened(object sender, EventArgs e)
        {

        }
    }
}