using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
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

		public int AlphaRangeMinimum { get; set; } = 10;
		public int AlphaRangeMaximum { get; set; } = 255;

        public string SubmitButtonText { get; } = "Submit";

		public string CloseButtonText { get; } = "Close";

        private AlgorithmConfig algorithmConfigSettings;


		public AlgorithmConfig AlgorithmConfigSettings
        {
			get { return algorithmConfigSettings; }
			set { algorithmConfigSettings = value; OnPropertyChanged(); }
		}

		public SettingsWindowModel(AlgorithmConfig currentAlgorithmConfig)
		{
			if(currentAlgorithmConfig != null)
                AlgorithmConfigSettings = currentAlgorithmConfig.Clone();
			else
                AlgorithmConfigSettings = new AlgorithmConfig();

			DrawPreview();
		}

        public double PrecentageOfScale 
		{ 
			get => AlgorithmConfigSettings.geneConfig.maxGeneScale * 100;
			set
			{
                AlgorithmConfigSettings.geneConfig.maxGeneScale = value / 100;
				OnPropertyChanged();
			}
		
		}

        public int AlphaMin 
		{ 
			get => AlgorithmConfigSettings.geneConfig.minAlpha;
			set 
			{

				int alphaMinVal = Math.Clamp(value, AlphaRangeMinimum, AlphaRangeMaximum);

				if(alphaMinVal > AlphaMax)
				{
					AlphaMax = alphaMinVal;
				}

                AlgorithmConfigSettings.geneConfig.minAlpha = value;

				DrawPreview();
                OnPropertyChanged();
			}
		}

        public int AlphaMax
		{
			get => AlgorithmConfigSettings.geneConfig.maxAlpha;
			set
			{
				int alphaMaxVal = Math.Clamp(value, AlphaRangeMinimum, AlphaRangeMaximum);

				if(alphaMaxVal < AlphaMin)
				{
					AlphaMin = alphaMaxVal;
				}

				AlgorithmConfigSettings.geneConfig.maxAlpha = value;

                DrawPreview();
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
            AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Rectangle;
		}

		private void EllipseChecked()
		{
            AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Ellipse;
		}

		private void TriangleChecked()
		{
            AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Triangle;
		}

		private void TournametChecked()
		{

		}


		// Change preview after submit
		private void SubmitSettings()
		{
			SubmitChanges?.Invoke(AlgorithmConfigSettings.Clone());
			DrawPreview();
		}

		private void DrawPreview()
		{
            int canvasHeight = 200;
            int canvasWidth = 200;
            Draw draw = new Draw(canvasHeight, canvasWidth, new PixelColor(255, 255, 255, 255));

			GeneFactoryConfig geneFactoryConfig = new GeneFactoryConfig
				(
					algorithmConfigSettings.geneConfig,
					canvasWidth,
					canvasHeight
				);

            Chromosome dummyChromosome = new Chromosome
                (
                    20,
                    canvasWidth,
                    canvasHeight,
					geneFactoryConfig

                );

			Individual individual = new Individual(dummyChromosome);

			PreviewImage = draw.RenderChromosome(individual);
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
