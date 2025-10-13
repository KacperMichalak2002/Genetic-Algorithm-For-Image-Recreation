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

        public static void Mutate(Individual individual, double mutationRate = 0.1)
        {
            foreach (Gene gene in individual.Chromosome.genes)
            {
                if (random.NextDouble() < mutationRate)
                {
                    MutateColor(gene);
                }
            }
        }
        public static void MutateColor(Gene gene)
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
    }
}
