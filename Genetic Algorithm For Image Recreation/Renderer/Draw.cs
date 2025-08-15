using Genetic_Algorithm_For_Image_Recreation.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class Draw
    {
        private double maxHeight, maxWidth;

        public Draw(double maxHeight, double maxWidth)
        {
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
        }

        public RenderTargetBitmap RenderChromosome(List<Chromosome> population, TextBlock textBlock)
        {
         
            DrawingVisual drawingVisual = new DrawingVisual();

            using(DrawingContext drawingContext = drawingVisual.RenderOpen())
            {

                foreach(Chromosome chromosome in population)
                {
                    foreach (var gene in chromosome.genes)
                    {
                        Brush brush = new SolidColorBrush(gene.color);

                        switch (gene.ShapeType)
                        {
                            case ShapeType.Rectangle:
                                drawingContext.DrawRectangle(brush, null, new Rect(gene.X, gene.Y, gene.width, gene.height));
                                break;
                            case ShapeType.Ellipse:
                                drawingContext.DrawEllipse(brush, null, new Point(gene.X, gene.Y), gene.width / 2, gene.height / 2);
                                break;
                            case ShapeType.Triangle:
                                StreamGeometry triangle = new StreamGeometry();
                                using (StreamGeometryContext context = triangle.Open())
                                {
                                    context.BeginFigure(gene.points[0], true, true);
                                    context.PolyLineTo(gene.points.Skip(1).ToList(), true, true);
                                }
                                drawingContext.DrawGeometry(brush, null, triangle);
                                break;
                        }
                    }
                }
                
            }

            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)maxWidth, (int)maxHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingVisual);


            textBlock.Text = ImageHandler.Test(bitmap);

            return bitmap;
        }
    }
}
