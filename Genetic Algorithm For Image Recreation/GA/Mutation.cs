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

            if(mutationType == 0)
            {
                MutateColor(gene);
                return;
            }

            if(mutationType == 1)
            {
                MutateAlpha(gene);
                return;
            }

            if(gene.ShapeType == ShapeType.Triangle)
            {
                MutateTrianglePoints(gene, maxHeight, maxWidth);
                return;
            }
            else
            {
                if(mutationType == 2)
                {
                    MutatePosition(gene, maxHeight, maxWidth);
                }
                else
                {
                    MutateSize(gene, maxHeight, maxWidth);
                }
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
            int diffA = random.Next(-30, 31);
            byte newA = (byte) Math.Clamp((gene.color.A + diffA), 30, 255);

            gene.color = Color.FromArgb(newA, gene.color.R, gene.color.G, gene.color.B);
        }

        private static void MutatePosition(Gene gene, int maxHeight, int maxWidth)
        {
            double mutationStrength = 0.2;
            double randomVal = random.NextDouble() - 0.5; // Range of [-0.5, 0.5]
            double maxShift = maxWidth * mutationStrength; // 20% of the image
            double diffX = randomVal * maxShift;

            double diffY = (random.NextDouble() - 0.5) * maxHeight * mutationStrength;

            gene.X = (int)Math.Clamp(gene.X + diffX, 0, maxWidth - gene.width);
            gene.Y = (int)Math.Clamp(gene.Y + diffY, 0, maxHeight - gene.height);

        }

        private static void MutateSize(Gene gene, int maxHeight, int maxWidth)
        {
            double widhtMult = 0.8 + random.NextDouble() * 0.4;
            double heightMult = 0.8 + random.NextDouble() * 0.4;

            gene.width = (int)Math.Clamp(gene.width * widhtMult, 5, maxWidth * 0.2);
            gene.height = (int)Math.Clamp(gene.height * heightMult, 5, maxHeight * 0.2);

            if(gene.X + gene.width > maxWidth)
            {
                gene.width = maxWidth - gene.X;
            }

            if(gene.Y + gene.height > maxHeight)
            {
                gene.height = maxHeight - gene.Y;
            }
        }

        private static void MutateTrianglePoints(Gene gene, int maxHeight, int maxWidth)
        {
            int numberOfPoints = gene.points.Count;
            double mutationStrength = 0.05;
            double procentOfImage = 0.20;
            List<BasicPoint> mutatedPoints = new List<BasicPoint>();

            int maxAllowedWidth = (int)(maxWidth * procentOfImage);
            int maxAllowedHeight = (int)(maxHeight * procentOfImage);

            maxAllowedWidth = Math.Max(maxAllowedWidth, 10);
            maxAllowedHeight = Math.Max(maxAllowedHeight, 10);

            for(int i = 0; i < numberOfPoints; i++)
            {
                double diffX = (random.NextDouble() - 0.5) * maxWidth * mutationStrength;
                double diffY = (random.NextDouble() - 0.5) * maxHeight * mutationStrength;

                int X = (int)Math.Clamp(gene.points[i].X + diffX, 0, maxWidth - 1);
                int Y = (int)Math.Clamp(gene.points[i].Y + diffY, 0, maxHeight - 1);

                BasicPoint point = new BasicPoint(X, Y);
                mutatedPoints.Add(point);
            }

            int minX = mutatedPoints.Min(p => p.X);
            int minY = mutatedPoints.Min(p => p.Y);
            int maxX = mutatedPoints.Max(p => p.X);
            int maxY = mutatedPoints.Max(p => p.Y);

            int newWidth = maxX - minX + 1;
            int newHeight = maxY - minY + 1;

            if(newWidth <= maxAllowedWidth && newHeight <= maxAllowedHeight)
            {
                gene.points = mutatedPoints;
                RecalculateBoundingBox(gene, maxHeight, maxWidth);
            }
            
        }

        private static void RecalculateBoundingBox(Gene gene, int maxHeight, int maxWidth)
        {
            int minX = gene.points.Min(p => p.X);
            int minY = gene.points.Min(p => p.Y);
            int maxX = gene.points.Max(p => p.X);
            int maxY = gene.points.Max(p => p.Y);

            gene.X = minX;
            gene.Y = minY;

            gene.width = Math.Clamp(maxX - minX + 1, 1, maxWidth - minX);
            gene.height = Math.Clamp(maxY - minY + 1, 1, maxHeight - minY);
        }
    }
}
