using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using Genetic_Algorithm_For_Image_Recreation.View;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private bool running = false;
        private CancellationTokenSource? cancellationTokenSource;
		private FormatConvertedBitmap? convertedImage;
		private Window parentWindow;
		private AlgorithmConfig currentAlgorithmConfig = new AlgorithmConfig();
		private IDialogCoordinator dialogCoordinator;


        public RelayCommand ToggleRunCommand => new RelayCommand(execute => Start(), canExecute => SourceImage != null);
		public RelayCommand LoadImageCommand => new RelayCommand(execute => LoadSourceImage(), canExecute => !running);

		public RelayCommand OpenSettingsCommand => new RelayCommand(execute => OpenSettings(), canExecute => !running);
		public RelayCommand SaveResultCommand => new RelayCommand(execute => SaveResult(), canExecute => !running);


        public MainWindowViewModel(Window window, IDialogCoordinator dialogCoordinatorInstance)
		{
			parentWindow = window;
			StatusText = "Load Image";
			ProgressText = "";
			StartButtonText = "Start";
			dialogCoordinator = dialogCoordinatorInstance;
		}

		public string SaveResultButtonText { get; } = "Save";

        private string statusText;
        public string StatusText	
		{
			get { return statusText ; }
			set 
			{ 
				statusText = value;
				OnPropertyChanged();
			}
		}

		private string progressText;

		public string ProgressText
		{
			get { return progressText; }
			set 
			{ 
				progressText = value; 
				OnPropertyChanged();
			}
		}

		private string startButtonText;

		public string StartButtonText
		{
			get { return startButtonText; }
			set { 
				startButtonText = value;
				OnPropertyChanged();
			}
		}

		public string SelectImageButtonText { get; } = "Select Image";

		public string SettingButtonText { get; } = "Settings";

        private ImageSource sourceImage;

		public ImageSource SourceImage
        {
			get { return sourceImage; }
			set 
			{ 
				sourceImage = value;
				OnPropertyChanged();
			}
		}

		private ImageSource resultImage;

		public ImageSource ResultImage
		{
			get { return resultImage; }
			set 
			{ 
				resultImage = value;
				OnPropertyChanged();
			}
		}


		private int progressValue = 0;

		public int ProgressValue
		{
			get { return progressValue; }
			set 
			{ 
				progressValue = value;
				OnPropertyChanged();
			}
		}

		private int progressMaximumValue = 100;
		public int ProgressMaximumValue
		{
			get { return progressMaximumValue; }
			set 
			{ 
				progressMaximumValue = value;
				OnPropertyChanged();
			}
		}



		private void LoadSourceImage()
		{
            var fileBrowser = new OpenFileDialog();
            fileBrowser.Filter = "Image Files| *.jpg;*.png";

            Boolean? result = fileBrowser.ShowDialog();

            if (result.Equals(true))
            {
                BitmapImage image = new BitmapImage();

                image.BeginInit();
                image.UriSource = new Uri(fileBrowser.FileName);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.DecodePixelHeight = 250; //Change to has and given amount of pixels in height
                image.EndInit();

                convertedImage = new FormatConvertedBitmap();
                convertedImage.BeginInit();
                convertedImage.Source = image;
                convertedImage.DestinationFormat = PixelFormats.Bgra32;
                convertedImage.EndInit();


                Debug.WriteLine($"Loaded image height {convertedImage.PixelHeight} Width {convertedImage.PixelWidth}");
                //ImageHandler.RectangleScanningSource(convertedImage);

                SourceImage = convertedImage;
				StatusText = "Load Image";
				ProgressText = "";
				ProgressValue = 0;

            }
        }

		private async void Start()
		{
			if (running)
			{
				if(cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
					StatusText = "Stopping";
				}
				running = false;
			}
			else
			{
				running = true;
				StartButtonText = "Stop";
				StatusText = "Running";
				ProgressMaximumValue = currentAlgorithmConfig.numberOfIterations;

				cancellationTokenSource = new CancellationTokenSource();
				CancellationToken cancellationToken = cancellationTokenSource.Token;

				try
				{

					

					PixelColor[] sourcePixels = ImageHandler.GetAllPxielsFromBitmap(convertedImage);

					PixelColor backgroundColor =ImageHandler.GetBackrgoundColor(sourcePixels) ;
                    Draw draw = new Draw(convertedImage.PixelHeight, convertedImage.PixelWidth, backgroundColor);


                    var progressHandler = new Progress<(int generationNumber, Individual best)>(report =>
                    {
                        draw.RenderChromosome(report.best);
                        ResultImage = draw.CloneCurrentBitmap();
                        ProgressText = $"{report.generationNumber} out of {ProgressMaximumValue}";
                        ProgressValue = report.generationNumber;

                    });

                    currentAlgorithmConfig.bitmapHeight = convertedImage.PixelHeight;
                    currentAlgorithmConfig.bitmapWidth = convertedImage.PixelWidth;
					currentAlgorithmConfig.sourcePixels = sourcePixels;

					GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(currentAlgorithmConfig);
					await Task.Run(() => geneticAlgorithm.Start(cancellationToken, progressHandler), cancellationToken);

					StatusText = "Finshed";

				}
				catch (OperationCanceledException)
				{
					StatusText = "Stopped by user";
				}
				finally
				{
					running = false;
					StartButtonText = "Start";
				}

			}


		}

		private void OpenSettings()
		{
			SettingsWindowModel settingsWindowModel = new SettingsWindowModel(currentAlgorithmConfig);

			SettingsWindow settingsWindow = new SettingsWindow(settingsWindowModel);
			settingsWindow.Owner = parentWindow;
			

			settingsWindowModel.SubmitChanges = (newConfig) =>
			{
				currentAlgorithmConfig = newConfig;
			};

			settingsWindow.ShowDialog();
		}

		private async Task SaveResult()
		{
			string messageBoxText = "Do you want to save result ?";
			string caption = "Saving";


			MetroDialogSettings dialogSettings = new MetroDialogSettings()
			{
				AffirmativeButtonText = "Save",
				NegativeButtonText = "Cancel",

			};

			var result = await dialogCoordinator.ShowMessageAsync(
				this,
				caption,
				messageBoxText,
				MessageDialogStyle.AffirmativeAndNegative,
				dialogSettings);

			switch (result)
			{
				case MessageDialogResult.Affirmative:
					// SAVE 
					break;
				case MessageDialogResult.Negative:
					break;
			}


		}

    }
}
