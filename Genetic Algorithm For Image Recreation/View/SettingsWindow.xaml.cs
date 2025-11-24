using ControlzEx.Theming;
using Genetic_Algorithm_For_Image_Recreation.ViewModel;
using System.Windows;

namespace Genetic_Algorithm_For_Image_Recreation.View
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(Window parent)
        {
            InitializeComponent();

            SettingsWindowModel viewModel = new SettingsWindowModel();
            DataContext = viewModel;


            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }
    }
}
