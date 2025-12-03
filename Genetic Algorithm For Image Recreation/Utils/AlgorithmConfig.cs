using Genetic_Algorithm_For_Image_Recreation.Renderer;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    public  class AlgorithmConfig
    {
        public int sizeOfPopulation { get; set; } = 10;
        public int numberOfIterations { get; set; } = 5000;
        public int bitmapHeight { get; set; }
        public int bitmapWidth { get; set; }
        public GeneConfig geneConfig { get; set; } = new GeneConfig();
        public PixelColor[] sourcePixels { get; set; }


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
            clone.geneConfig = this.geneConfig.Clone();

            return clone;
        }
    }
}
