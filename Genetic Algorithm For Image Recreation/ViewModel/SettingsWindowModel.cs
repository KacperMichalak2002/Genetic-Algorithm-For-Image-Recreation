using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Utils;

namespace Genetic_Algorithm_For_Image_Recreation.ViewModel
{
    internal class SettingsWindowModel : ViewModelBase
    {
		public Action<bool> RequestClose { get; set; }

		public RelayCommand SubmitCommand => new RelayCommand(execute => SubmitSettings(), canExecute => CanSubmit());
		public RelayCommand RectangleCheckedCommand => new RelayCommand(excecute => RectangleChecked());
        public RelayCommand EllipseCheckedCommand => new RelayCommand(excecute => EllipseChecked());
        public RelayCommand TriangleCheckedCommand => new RelayCommand(excecute => TriangleChecked());
		public RelayCommand TournamentCheckedCommand => new RelayCommand(execute => TournametChecked());

		public int PopulationMinimum { get; } = 5;
        public int PopulationMaximum { get; } = 100;
        public int GenesMinimum { get; } = 5;
		public int GenesMaximum { get; } = 10_000;
		public int IterationsMinimum { get; } = 100;
		public int IterationsMaximum { get; } = 50_000;
		public int SizeMinimum { get; } = 2;
		public int SizeMaximum { get; } = 30;

        private AlgorithmConfig algorithmConfig;


		public AlgorithmConfig AlgorithmConfig
        {
			get { return algorithmConfig; }
			set { algorithmConfig = value; OnPropertyChanged(); }
		}

		public SettingsWindowModel()
		{

		}


		private void RectangleChecked()
		{
			AlgorithmConfig.shapeType = ShapeType.Rectangle;
		}

		private void EllipseChecked()
		{
            AlgorithmConfig.shapeType = ShapeType.Ellipse;
		}

		private void TriangleChecked()
		{
            AlgorithmConfig.shapeType = ShapeType.Triangle;
		}

		private void TournametChecked()
		{

		}

		private void SubmitSettings()
		{
			RequestClose?.Invoke(true);
		}

		private bool CanSubmit()
		{
			return true;
		}


	}
}
