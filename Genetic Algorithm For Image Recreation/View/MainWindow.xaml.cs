using ControlzEx.Theming;
using Genetic_Algorithm_For_Image_Recreation.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace Genetic_Algorithm_For_Image_Recreation
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(this, DialogCoordinator.Instance);
            DataContext = viewModel;

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }
    }
}