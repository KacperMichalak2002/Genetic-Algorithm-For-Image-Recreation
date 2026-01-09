using Genetic_Algorithm_For_Image_Recreation.Model.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.Model.Config
{
    public class GeneConfig
    {

        public int numberOfGenes { get; set; } = 2000;
        public double maxGeneScale { get; set; } = 0.10;
        public ShapeType shapeType { get; set; } = ShapeType.Ellipse;
        public PixelColor backgroundColor { get; set; } = new PixelColor(255, 255, 255, 255);

        public int maxAlpha { get; set; } = 200;
        public int minAlpha { get; set; } = 70;



        public GeneConfig()
        {

        }

        public GeneConfig(int numberOfGenes , double maxGeneScale, ShapeType shapeType, PixelColor backgroundColor)
        {
            this.numberOfGenes = numberOfGenes;
            this.maxGeneScale = maxGeneScale;
            this.shapeType = shapeType;
            this.backgroundColor = backgroundColor;
        }

        public GeneConfig Clone()
        {
            return (GeneConfig)MemberwiseClone();
        }
    }
}
