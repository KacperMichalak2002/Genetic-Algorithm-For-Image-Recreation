using Genetic_Algorithm_For_Image_Recreation.GA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Genetic_Algorithm_For_Image_Recreation.Renderer
{
    internal class Draw
    {
        public Canvas algorithmCanva;
        private double maxHeight, maxWidth;

        public Draw(Canvas canvas, double maxHeight, double maxWidth)
        {
            algorithmCanva = canvas;
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
        }

        public void StartDrawing()
        {
            Chromosome chromosome = new Chromosome(3, maxWidth, maxHeight);
            foreach(var gene in chromosome.genes)
            {
                Ellipse ellipse = new Ellipse { 
                    Width = gene.width,
                    Height = gene.height,
                    Fill = new SolidColorBrush(gene.color)
                };

                Canvas.SetLeft(ellipse, gene.X);
                algorithmCanva.Background = new SolidColorBrush(gene.backgroundColor);
                algorithmCanva.Children.Add(ellipse);
            }
        }
    }
}
