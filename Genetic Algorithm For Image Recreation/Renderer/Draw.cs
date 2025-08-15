using Genetic_Algorithm_For_Image_Recreation.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class Draw
    {
        public Canvas algorithmCanva;
        private double maxHeight, maxWidth;
        private ShapeType shapeType;

        public Draw(Canvas canvas, double maxHeight, double maxWidth, ShapeType shapeType)
        {
            algorithmCanva = canvas;
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
            this.shapeType = shapeType;
        }

        public void StartDrawing()
        {
            Chromosome chromosome = new Chromosome(100, maxWidth, maxHeight, shapeType);
            foreach(var gene in chromosome.genes)
            {

                Shape newShape = GetShape(shapeType);

                switch (newShape)
                {
                    case Ellipse:
                    case Rectangle:
                        newShape.Width = gene.width;
                        newShape.Height = gene.height;
                        break;
                    case Polygon polygon:
                        polygon.Points = gene.points;
                        break;

                }

                newShape.Fill = new SolidColorBrush(gene.color);

                Canvas.SetLeft(newShape, gene.X);
                Canvas.SetTop(newShape, gene.Y);
                algorithmCanva.Background = new SolidColorBrush(gene.backgroundColor);
                algorithmCanva.Children.Add(newShape);
            }
        }

        public Shape GetShape(ShapeType shapeType) {


            switch (shapeType)
            {
                case ShapeType.Rectangle:
                    return new Rectangle();
                case ShapeType.Ellipse:
                    return new Ellipse();
                case ShapeType.Triangle:
                    return new Polygon();
            }

            throw new NotSupportedException("Not supported shape was given");
        
        }
    }
}
