namespace Genetic_Algorithm_For_Image_Recreation.Model.Config
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
            maxHeight = (int)(imageWidth * geneConfig.maxGeneScale);
            maxWidth = (int)(imageHeight * geneConfig.maxGeneScale);
            maxAlpha = geneConfig.maxAlpha;
            minAlpha = geneConfig.minAlpha;
            shapeType = geneConfig.shapeType;
        }



    }
}
