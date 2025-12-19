using Aspose.Svg.Drawing;
using Genetic_Algorithm_For_Image_Recreation.GA;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            foreach(var gene in individual.Chromosome.genes)
            {
                switch (gene.ShapeType)
                {
                    case ShapeType.Rectangle:
                        RectangleSaving(stringBuilder, gene);
                        break;
                    case ShapeType.Ellipse:
                        EllipseSaving(stringBuilder, gene);
                        break;
                    case ShapeType.Triangle:
                        TrinagleSaving(stringBuilder, gene);
                        break;

                }

            }

            stringBuilder.AppendLine("</svg>");

           File.WriteAllText(filePath, stringBuilder.ToString());
        }

        private static void RectangleSaving(StringBuilder stringBuilder, Gene gene)
        {
            var color = Color.FromRgba(gene.color.R, gene.color.G, gene.color.B, gene.color.A);
            string hexColor = color.ToRgbaHexString();

            stringBuilder.AppendLine($"<rect width=\"{gene.width}\" height=\"{gene.height}\" x=\"{gene.X}\" y=\"{gene.Y}\" fill=\"{hexColor}\" />");
        }

        private static void EllipseSaving(StringBuilder stringBuilder, Gene gene)
        {
            var color = Color.FromRgba(gene.color.R, gene.color.G, gene.color.B, gene.color.A);
            string hexColor = color.ToRgbaHexString();

            int rx = gene.width / 2;
            int ry = gene.height / 2;
            int cx = gene.X + rx;
            int cy = gene.Y + ry;

            stringBuilder.AppendLine($"<ellipse cx=\"{cx}\" cy=\"{cy}\" rx=\"{rx}\" ry=\"{ry}\" fill=\"{hexColor}\" />");
        }

        private static void TrinagleSaving(StringBuilder stringBuilder, Gene gene)
        {
            var color = Color.FromRgba(gene.color.R, gene.color.G, gene.color.B, gene.color.A);
            string hexColor = color.ToRgbaHexString();

            BasicPoint p1 = gene.points[0];
            BasicPoint p2 = gene.points[1];
            BasicPoint p3 = gene.points[2];

            stringBuilder.AppendLine($"<polygon points=\"{p1.X},{p1.Y} {p2.X},{p2.Y} {p3.X},{p3.Y}\" fill=\"{hexColor}\" />");
        }

    }
}
