using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.ViewModel
{
    internal class SettingsWindowModel : ViewModelBase
    {
        private IDialogCoordinator dialogCoordinator;
        public Action<bool> RequestClose { get; set; }
		public Action<AlgorithmConfig> SubmitChanges { get; set; }

		public RelayCommand SubmitCommand => new RelayCommand(execute => SubmitSettings(), canExecute => CanSubmit());
		public RelayCommand CloseCommand => new RelayCommand(execute => CloseSettings());
		public RelayCommand TournamentCheckedCommand => new RelayCommand(execute => TournametChecked());

		public int PopulationMinimum { get; } = 1;
        public int PopulationMaximum { get; } = 100;
        public int GenesMinimum { get; } = 1;
		public int GenesMaximum { get; } = 10_000;
		public int IterationsMinimum { get; } = 100;
		public int IterationsMaximum { get; } = 500_000;
		public int SizeMinimum { get; } = 2;
		public int SizeMaximum { get; } = 50;
		public int MutationRateMinimum { get; } = 1;
		public int MutationRateMaximum { get; } = 50;

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

		public SettingsWindowModel(AlgorithmConfig currentAlgorithmConfig, IDialogCoordinator dialogCoordinatorInstance)
		{
			if(currentAlgorithmConfig != null)
                AlgorithmConfigSettings = currentAlgorithmConfig.Clone();
			else
                AlgorithmConfigSettings = new AlgorithmConfig();

			dialogCoordinator = dialogCoordinatorInstance;

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

		public double PrecentOfMutationRate
		{
			get => AlgorithmConfigSettings.mutationRate * 100;
			set
			{
				AlgorithmConfigSettings.mutationRate = value / 100; 
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

        public bool RectangleCheck 
		{ 
			get => AlgorithmConfigSettings.geneConfig.shapeType == ShapeType.Rectangle;
			set
			{
				if (value)
				{
					AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Rectangle;
					OnPropertyChanged();
					OnPropertyChanged(nameof(EllipseCheck));
                    OnPropertyChanged(nameof(TriangleCheck));

                    DrawPreview();
                }
			}
		}

        public bool EllipseCheck 
		{ 
			get => AlgorithmConfigSettings.geneConfig.shapeType == ShapeType.Ellipse; 
			set
			{
				if (value)
				{
					AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Ellipse;
					OnPropertyChanged();
					OnPropertyChanged(nameof(RectangleCheck));
                    OnPropertyChanged(nameof(TriangleCheck));

					DrawPreview();
                }
			}
		}

		public bool TriangleCheck
		{
			get => AlgorithmConfigSettings.geneConfig.shapeType == ShapeType.Triangle;
			set
			{
				if (value)
				{
					AlgorithmConfigSettings.geneConfig.shapeType = ShapeType.Triangle;
					OnPropertyChanged();
					OnPropertyChanged(nameof(RectangleCheck));
                    OnPropertyChanged(nameof(EllipseCheck));

                    DrawPreview();
                }
			}

        }

		// Change after adding new selections
        public bool TournamentCheck 
		{ 
			get => true; 
			set
			{

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

		private void TournametChecked()
		{

		}


		// Change preview after submit
		private async Task SubmitSettings()
		{

			string messageBoxText = "Changes applied";
			string caption = "Parameters changed";

			MetroDialogSettings dialogSettings = new MetroDialogSettings()
			{ 
				AffirmativeButtonText = "Ok"
			};

			var messageBoxResult = await dialogCoordinator.ShowMessageAsync(
				this,
				caption,
				messageBoxText,
				MessageDialogStyle.Affirmative,
				dialogSettings
				);

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
                    50,
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
