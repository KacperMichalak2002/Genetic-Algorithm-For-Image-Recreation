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
        private Shape shape;

        public Draw(Canvas canvas, double maxHeight, double maxWidth, Shape shape)
        {
            algorithmCanva = canvas;
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
            this.shape = shape;
        }

        public void StartDrawing()
        {
            Chromosome chromosome = new Chromosome(100, maxWidth, maxHeight, shape );
            foreach(var gene in chromosome.genes)
            {

                Shape newShape = GetShape(shape);

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

        public Shape GetShape(Shape prototype) {

            if (prototype is Ellipse)
                return new Ellipse();

            if(prototype is Polygon)
                return new Polygon();

            if(prototype is Rectangle)
                return new Rectangle();

            throw new NotSupportedException("Not supported shape was given");
        
        }
    }
}
