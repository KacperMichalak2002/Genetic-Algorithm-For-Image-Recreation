using ControlzEx.Theming;
using Genetic_Algorithm_For_Image_Recreation.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace Genetic_Algorithm_For_Image_Recreation.View
{
    public partial class SettingsWindow
    {
        internal SettingsWindow(SettingsWindowModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.RequestClose = (result) =>
            {
                this.Close();
            };

            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }
    }
}
