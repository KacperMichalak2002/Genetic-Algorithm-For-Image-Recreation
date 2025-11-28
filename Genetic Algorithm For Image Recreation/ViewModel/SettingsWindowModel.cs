using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using System.Diagnostics;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.ViewModel
{
    internal class SettingsWindowModel : ViewModelBase
    {
		public Action<bool> RequestClose { get; set; }
		public Action<AlgorithmConfig> SubmitChanges { get; set; }

		public RelayCommand SubmitCommand => new RelayCommand(execute => SubmitSettings(), canExecute => CanSubmit());
		public RelayCommand CloseCommand => new RelayCommand(execute => CloseSettings());
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

		public string SubmitButtonText { get; } = "Submit";

		public string CloseButtonText { get; } = "Close";

        private AlgorithmConfig algorithmConfig;


		public AlgorithmConfig AlgorithmConfig
        {
			get { return algorithmConfig; }
			set { algorithmConfig = value; OnPropertyChanged(); }
		}

		public SettingsWindowModel(AlgorithmConfig currentAlgorithmConfig)
		{
			if(currentAlgorithmConfig != null)
				AlgorithmConfig = currentAlgorithmConfig.Clone();
			else
				AlgorithmConfig = new AlgorithmConfig();
		}

        public double PrecentageOfScale 
		{ 
			get => AlgorithmConfig.geneConfig.maxGeneScale * 100;
			set
			{
				AlgorithmConfig.geneConfig.maxGeneScale = value / 100;
				OnPropertyChanged();
			}
		
		}

		private ImageSource previewImage;

		public ImageSource PreviewImage
		{
			get { return previewImage; }
			set 
			{ 
				previewImage = value; 
				OnPropertyChanged();
			}
		}



		private void RectangleChecked()
		{
			AlgorithmConfig.geneConfig.shapeType = ShapeType.Rectangle;
		}

		private void EllipseChecked()
		{
            AlgorithmConfig.geneConfig.shapeType = ShapeType.Ellipse;
		}

		private void TriangleChecked()
		{
            AlgorithmConfig.geneConfig.shapeType = ShapeType.Triangle;
		}

		private void TournametChecked()
		{

		}


		// Change preview after submit
		private void SubmitSettings()
		{
			SubmitChanges?.Invoke(AlgorithmConfig.Clone());
		}

		private void CloseSettings()
		{
            RequestClose?.Invoke(false);
        }

        private bool CanSubmit()
		{
			return true;
		}


	}
}
