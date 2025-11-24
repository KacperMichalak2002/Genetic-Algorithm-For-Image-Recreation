using Genetic_Algorithm_For_Image_Recreation.GA;
using Genetic_Algorithm_For_Image_Recreation.MVVM;
using Genetic_Algorithm_For_Image_Recreation.Renderer;
using Genetic_Algorithm_For_Image_Recreation.Utils;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        // Change to get from user interface
        int numberOfIterations = 10_000;
        int numberOfGenes = 500;
        int populationSize = 100;
        double maxGeneScale = 0.07; // in % so 0.05 is 5% of image
		ShapeType shapeType = ShapeType.Ellipse;

        private bool running = false;
        private CancellationTokenSource? cancellationTokenSource;
		private FormatConvertedBitmap? convertedImage;


        public RelayCommand ToggleRunCommand => new RelayCommand(execute => Start(), canExecute => SourceImage != null);
		public RelayCommand LoadImageCommand => new RelayCommand(execute => LoadSourceImage());


        public MainWindowViewModel()
		{
			StatusText = "Load Image";
			ProgressText = "";
			StartButtonText = "Start";
			SelectImageButtonText = "Load Image";
		}

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

		private string selectImagebuttonText;

		public string SelectImageButtonText
		{
			get { return selectImagebuttonText; }
			set 
			{ 
				selectImagebuttonText = value;
				OnPropertyChanged();
			}
		}



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
				ProgressMaximumValue = numberOfIterations;

				cancellationTokenSource = new CancellationTokenSource();
				CancellationToken cancellationToken = cancellationTokenSource.Token;

				try
				{
					PixelColor[] sourcePixels = ImageHandler.GetAllPxielsFromBitmap(convertedImage);
					Draw draw = new Draw(convertedImage.PixelHeight, convertedImage.PixelWidth);


                    var progressHandler = new Progress<(int generationNumber, Individual best)>(report =>
                    {
                        draw.RenderChromosome(report.best);
                        ResultImage = draw.CloneCurrentBitmap();
                        ProgressText = $"{report.generationNumber} out of {numberOfIterations}";
                        ProgressValue = report.generationNumber;

                    });

					AlgorithmConfig algorithmConfig = new AlgorithmConfig
						(
							populationSize,
							numberOfGenes,
							numberOfIterations,
							convertedImage.PixelHeight,
							convertedImage.PixelWidth,
							maxGeneScale,
							shapeType,
							sourcePixels
						);

					GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(algorithmConfig);
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
	}
}
