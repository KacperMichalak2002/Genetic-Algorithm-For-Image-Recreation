using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Mutation
    {
        private static Random random = new Random();

        public static void Mutate(Individual individual, double mutationRate)
        {
            int maxWidth = individual.Chromosome.imageWidth;
            int maxHeight = individual.Chromosome.imageHeight;

            foreach (Gene gene in individual.Chromosome.genes)
            {
                if (random.NextDouble() < mutationRate)
                {
                    SelectMutation(gene,maxHeight,maxWidth);
                }
            }
        }

        private static void SelectMutation(Gene gene, int maxHeight, int maxWidth)
        {
            int mutationType = random.Next(0,4);

            switch (mutationType)
            {
                case 0:
                    MutateColor(gene);
                    break;
                case 1:
                    MutateAlpha(gene);
                    break;
                case 2:
                    MutatePosition(gene, maxHeight, maxWidth);
                    break;
                case 3:
                    MutateSize(gene, maxHeight, maxWidth);
                    break;

            }
        }

        private static void MutateColor(Gene gene)
        {
            int diffR = random.Next(-30, 31);
            int diffG = random.Next(-30, 31);
            int diffB = random.Next(-30, 31);


            byte newR = (byte)Math.Clamp((gene.color.R + diffR), 0, 255);
            byte newG = (byte)Math.Clamp((gene.color.G + diffG), 0, 255);
            byte newB = (byte)Math.Clamp((gene.color.B + diffB), 0, 255);

            Color newColor = Color.FromArgb(gene.color.A, newR, newG, newB);

            gene.color = newColor;
        }

        private static void MutateAlpha(Gene gene)
        {
            int diffA = random.Next(-50, 31);
            byte newA = (byte) Math.Clamp((gene.color.A + diffA), 50, 255);

            gene.color = Color.FromArgb(newA, gene.color.R, gene.color.G, gene.color.B);
        }

        private static void MutatePosition(Gene gene, int maxHeight, int maxWidth)
        {
            double randomVal = random.NextDouble() - 0.5; // Range of [-0.5, 0.5]
            double maxShift = maxWidth * 0.2; // 20% of the image
            double diffX = randomVal * maxShift;

            double diffY = (random.NextDouble() - 0.5) * maxHeight * 0.2;

            gene.X = (int)Math.Clamp(gene.X + diffX, 0, maxWidth - gene.width);
            gene.Y = (int)Math.Clamp(gene.Y + diffY, 0, maxHeight - gene.height);

        }

        private static void MutateSize(Gene gene, int maxHeight, int maxWidth)
        {
            double widhtMult = 0.8 + random.NextDouble() * 0.4;
            double heightMult = 0.8 + random.NextDouble() * 0.4;

            gene.width = (int)Math.Clamp(gene.width * widhtMult, 5, maxWidth * 0.5);
            gene.height = (int)Math.Clamp(gene.height * heightMult, 5, maxHeight * 0.5);

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
