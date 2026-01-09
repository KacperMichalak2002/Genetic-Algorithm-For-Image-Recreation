using Genetic_Algorithm_For_Image_Recreation.Model.Processing;

namespace Genetic_Algorithm_For_Image_Recreation.Model.Config
{
    public  class AlgorithmConfig
    {
        public int sizeOfPopulation { get; set; } = 10;
        public int numberOfIterations { get; set; } = 50000;
        public int bitmapHeight { get; set; }
        public int bitmapWidth { get; set; }
        public GeneConfig geneConfig { get; set; } = new GeneConfig();
        public PixelColor[] sourcePixels { get; set; }
        public double mutationRate { get; set; } = 0.02;
        public PixelColor backgroundColor { get; set; } = new PixelColor(255, 255, 255, 255);


        public AlgorithmConfig() { }
        public AlgorithmConfig(int sizeOfPopulation, int numberOfIterations, int bitmapHeight, int bitmapWidth, GeneConfig geneConfig, PixelColor[] sourcePixels)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.numberOfIterations = numberOfIterations;
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
            this.geneConfig = geneConfig;
            this.sourcePixels = sourcePixels;
        }

        public AlgorithmConfig Clone()
        {
            AlgorithmConfig clone = (AlgorithmConfig)MemberwiseClone();
            clone.geneConfig = geneConfig.Clone();

            return clone;
        }
    }
}
