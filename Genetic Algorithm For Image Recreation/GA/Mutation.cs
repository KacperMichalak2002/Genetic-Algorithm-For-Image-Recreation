using Genetic_Algorithm_For_Image_Recreation.Utils;
using System.Windows.Media;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Mutation
    {

        public static void Mutate(Individual individual, AlgorithmConfig algorithmConfig, Random random)
        {
            int maxWidth = individual.Chromosome.imageWidth;
            int maxHeight = individual.Chromosome.imageHeight;
            
            int geneCount = individual.Chromosome.genes.Count;
            int geneIndexToMutate = random.Next(0, geneCount);

            Gene geneToMuate = individual.Chromosome.genes[geneIndexToMutate];
            
            
            SelectMutation(geneToMuate, algorithmConfig, random);

        }

        private static void SelectMutation(Gene gene, AlgorithmConfig algorithmConfig, Random random)
        {
            int mutationType = random.Next(0,4);

            if(mutationType == 0)
            {
                MutateColor(gene, random);
                return;
            }

            if(mutationType == 1)
            {
                MutateAlpha(gene, algorithmConfig.geneConfig.minAlpha, algorithmConfig.geneConfig.maxAlpha ,random);
                return;
            }

            if(gene.ShapeType == ShapeType.Triangle)
            {
                MutateTrianglePoints(gene, algorithmConfig.bitmapHeight, algorithmConfig.bitmapWidth, algorithmConfig.geneConfig.maxGeneScale, random);
                return;
            }
            else
            {
                if(mutationType == 2)
                {
                    MutatePosition(gene, algorithmConfig.bitmapHeight, algorithmConfig.bitmapWidth, random);
                }
                else
                {
                    MutateSize(gene, algorithmConfig.bitmapHeight, algorithmConfig.bitmapWidth, algorithmConfig.geneConfig.maxGeneScale, random);
                }
            }   
        }

        private static void MutateColor(Gene gene, Random random)
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

        private static void MutateAlpha(Gene gene,int minAlpha, int maxAlpha, Random random)
        {
            int diffA = random.Next(-30, 31);
            byte newA = (byte) Math.Clamp((gene.color.A + diffA), minAlpha, maxAlpha);

            gene.color = Color.FromArgb(newA, gene.color.R, gene.color.G, gene.color.B);
        }

        private static void MutatePosition(Gene gene, int maxHeight, int maxWidth, Random random)
        {
            double mutationStrength = 0.2;
            double randomVal = random.NextDouble() - 0.5; // Range of [-0.5, 0.5]
            double maxShift = maxWidth * mutationStrength; // 20% of the image
            double diffX = randomVal * maxShift;

            double diffY = (random.NextDouble() - 0.5) * maxHeight * mutationStrength;

            gene.X = (int)Math.Clamp(gene.X + diffX, 0, maxWidth - gene.width);
            gene.Y = (int)Math.Clamp(gene.Y + diffY, 0, maxHeight - gene.height);

        }

        private static void MutateSize(Gene gene, int maxHeight, int maxWidth, double maxGeneScale, Random random)
        {
            double widhtMult = 0.8 + random.NextDouble() * 0.4;
            double heightMult = 0.8 + random.NextDouble() * 0.4;

            gene.width = (int)Math.Clamp(gene.width * widhtMult, 5, maxWidth * maxGeneScale);
            gene.height = (int)Math.Clamp(gene.height * heightMult, 5, maxHeight * maxGeneScale);

            if(gene.X + gene.width > maxWidth)
            {
                gene.width = maxWidth - gene.X;
            }

            if(gene.Y + gene.height > maxHeight)
            {
                gene.height = maxHeight - gene.Y;
            }
        }

        private static void MutateTrianglePoints(Gene gene, int maxHeight, int maxWidth, double maxGeneScale, Random random)
        {
            int numberOfPoints = gene.points.Count;
            double mutationStrength = 0.05;
            List<BasicPoint> mutatedPoints = new List<BasicPoint>();

            int maxAllowedWidth = (int)(maxWidth * maxGeneScale);
            int maxAllowedHeight = (int)(maxHeight * maxGeneScale);

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
