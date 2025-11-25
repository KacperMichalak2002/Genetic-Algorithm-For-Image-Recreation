using Genetic_Algorithm_For_Image_Recreation.Renderer;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    public  class AlgorithmConfig
    {
        public int sizeOfPopulation { get; set; } = 40;
        public int numberOfGenes { get; set; } = 500;
        public int numberOfIterations { get; set; } = 1000;
        public int bitmapHeight { get; set; }
        public int bitmapWidth { get; set; }
        public double maxGeneScale { get; set; } = 0.2;
        public ShapeType shapeType { get; set; } = ShapeType.Ellipse;
        public PixelColor[] sourcePixels { get; set; }


        public AlgorithmConfig() { }
        public AlgorithmConfig(int sizeOfPopulation, int numberOfGenes, int numberOfIterations, int bitmapHeight, int bitmapWidth, double maxGeneScale, ShapeType shapeType, PixelColor[] sourcePixels)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.numberOfGenes = numberOfGenes;
            this.numberOfIterations = numberOfIterations;
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
            this.maxGeneScale = maxGeneScale;
            this.shapeType = shapeType;
            this.sourcePixels = sourcePixels;
        }

        public AlgorithmConfig Clone()
        {
            return (AlgorithmConfig)MemberwiseClone();
        }
    }
}
