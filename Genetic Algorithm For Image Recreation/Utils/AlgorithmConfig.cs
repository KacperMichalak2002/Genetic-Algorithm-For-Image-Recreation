using Genetic_Algorithm_For_Image_Recreation.Renderer;

namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    public  class AlgorithmConfig
    {
        public int sizeOfPopulation { get; set; }
        public int numberOfGenes { get; set; }
        public int numberOfIterations { get; set; }
        public int bitmapHeight { get; set; }
        public int bitmapWidth { get; set; }
        public ShapeType shapeType { get; set; }
        public PixelColor[] sourcePixels { get; set; }

        public AlgorithmConfig(int sizeOfPopulation, int numberOfGenes, int numberOfIterations, int bitmapHeight, int bitmapWidth, ShapeType shapeType, PixelColor[] sourcePixels)
        {
            this.sizeOfPopulation = sizeOfPopulation;
            this.numberOfGenes = numberOfGenes;
            this.numberOfIterations = numberOfIterations;
            this.bitmapHeight = bitmapHeight;
            this.bitmapWidth = bitmapWidth;
            this.shapeType = shapeType;
            this.sourcePixels = sourcePixels;
        }
    }
}
