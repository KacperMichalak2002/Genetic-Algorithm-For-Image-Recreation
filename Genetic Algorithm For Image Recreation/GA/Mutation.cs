using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Mutation
    {
        private static Random random = new Random();

        public static void Mutate(Individual individual, double mutationRate = 0.1, double maxHeight = 100, double maxWidth = 100)
        {
            foreach (Gene gene in individual.Chromosome.genes)
            {
                if (random.NextDouble() < mutationRate)
                {
                    SelectMutation(gene,maxHeight,maxWidth);
                }
            }
        }

        private static void SelectMutation(Gene gene, double maxHeight, double maxWidth)
        {
            int mutationType = random.Next(0,3);

            switch (mutationType)
            {
                case 0:
                    MutateColor(gene);
                    break;
                case 1:
                    MutatePosition(gene, maxHeight, maxWidth); 
                    break;
                case 2:
                    MutateSize(gene, maxHeight, maxWidth);
                    break;

            }
        }

        private static void MutateColor(Gene gene)
        {
            int diffR = random.Next(-30, 30);
            int diffG = random.Next(-30, 30);
            int diffB = random.Next(-30, 30);


            byte newR = (byte)Math.Clamp((gene.color.R + diffR), 0, 255);
            byte newG = (byte)Math.Clamp((gene.color.G + diffG), 0, 255);
            byte newB = (byte)Math.Clamp((gene.color.B + diffB), 0, 255);

            Color newColor = Color.FromArgb(gene.color.A, newR, newG, newB);

            gene.color = newColor;
        }

        private static void MutatePosition(Gene gene, double maxHeight, double maxWidth)
        {
            double randomVal = random.NextDouble() - 0.5; // Range of [-0.5, 0.5]
            double maxShift = maxWidth * 0.2; // 20% of the image
            double diffX = randomVal * maxShift;

            double diffY = (random.NextDouble() - 0.5) * maxHeight * 0.2;

            gene.X = Math.Clamp(gene.X + diffX, 0, maxWidth);
            gene.Y = Math.Clamp(gene.Y + diffY, 0, maxHeight);
        }

        private static void MutateSize(Gene gene, double maxHeight, double maxWidth)
        {
            double widhtMult = 0.8 + random.NextDouble() * 0.4;
            double heightMult = 0.8 + random.NextDouble() * 0.4;

            gene.width = Math.Clamp(gene.width * widhtMult, 5, maxWidth * 0.5);
            gene.height = Math.Clamp(gene.height * heightMult, 5, maxHeight * 0.5);

            if(gene.X + gene.width > maxWidth)
            {
                gene.width = maxWidth - gene.X;
            }

            if(gene.Y + gene.height > maxHeight)
            {
                gene.height = maxHeight - gene.Y;
            }
        }



    }
}
