namespace Genetic_Algorithm_For_Image_Recreation.Utils
{
    public class GeneFactoryConfig
    {
        public int maxHeight { get; set; }
        public int maxWidth { get; set; }
        public int imageHeight { get; set; }
        public int imageWidth { get; set; }
        public int minAlpha { get; set; }
        public int maxAlpha { get; set; }
        public ShapeType shapeType { get; set; }

        public GeneFactoryConfig (GeneConfig geneConfig, int imageWidth, int imageHeight)
        {
            this.maxHeight = (int)(imageWidth * geneConfig.maxGeneScale);
            this.maxWidth = (int)(imageHeight * geneConfig.maxGeneScale);
            this.maxAlpha = geneConfig.maxAlpha;
            this.minAlpha = geneConfig.minAlpha;
            this.shapeType = geneConfig.shapeType;
        }



    }
}
