using Aspose.Svg.Drawing;
using Genetic_Algorithm_For_Image_Recreation.GA;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class SaveHandler
    {

        public static void SaveImageToPng(BitmapSource bitmapToSave, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapToSave));

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public static void SaveImageToSvg(Individual individual, PixelColor backgroundColor, string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();


            int width = individual.Chromosome.imageWidth;
            int height = individual.Chromosome.imageHeight;

            stringBuilder.AppendLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\">");
            stringBuilder.AppendLine($"<rect width=\"100%\" height=\"100%\" fill=\"rgb({backgroundColor.R},{backgroundColor.G},{backgroundColor.B},{backgroundColor.A})\" />");

            //Rectangle
            foreach(var shape in individual.Chromosome.genes)
            {
                if(shape.ShapeType == ShapeType.Rectangle)
                {
                    var color = Color.FromRgba(shape.color.R, shape.color.G, shape.color.B, shape.color.A);
                    string hexColor = color.ToRgbaHexString();

                    stringBuilder.AppendLine($"<rect width=\"{shape.width}\" height=\"{shape.height}\" x=\"{shape.X}\" y=\"{shape.Y}\" fill=\"{hexColor}\" />");
                }
                
            }

            stringBuilder.AppendLine("</svg>");

           File.WriteAllText(filePath, stringBuilder.ToString());
        }

    }
}
