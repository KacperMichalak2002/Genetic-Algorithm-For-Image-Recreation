using Genetic_Algorithm_For_Image_Recreation.GA;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class Draw
    {
        private double maxHeight, maxWidth;
        private RenderTargetBitmap renderTarget;

        public Draw(int maxHeight, int maxWidth)
        {
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
            renderTarget = new RenderTargetBitmap(maxWidth, maxHeight, 96, 96, PixelFormats.Pbgra32);
        }

        public RenderTargetBitmap RenderChromosome(Individual individual)
        {
         
            DrawingVisual drawingVisual = new DrawingVisual();

            using(DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(0, 0, maxWidth, maxHeight));

                foreach (var gene in individual.Chromosome.genes)
                    {
                        Brush brush = new SolidColorBrush(gene.color);

                        switch (gene.ShapeType)
                        {
                            case ShapeType.Rectangle:
                                drawingContext.DrawRectangle(brush, null, new Rect(gene.X, gene.Y, gene.width, gene.height));
                                break;
                            case ShapeType.Ellipse:
                                drawingContext.DrawEllipse(brush, null, new Point(gene.X + gene.width / 2, gene.Y + gene.height / 2), gene.width / 2, gene.height / 2);
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

            renderTarget.Clear();
            renderTarget.Render(drawingVisual);

            return renderTarget;
        }

        //private void ClearRenderTarget()
        //{
        //    DrawingVisual clearVisual = new DrawingVisual();
        //    using(DrawingContext dc = clearVisual.RenderOpen())
        //    {
        //        dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, maxWidth, maxHeight));
        //    }
        //    renderTarget.Render(clearVisual);
        //}

        public BitmapSource CloneCurrentBitmap()
        {
            return renderTarget.Clone();
        }
    }
}
