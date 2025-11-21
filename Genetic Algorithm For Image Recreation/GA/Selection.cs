using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Selection
    {
        private class FitnessCom : IComparer<Individual>
        {
            public int Compare(Individual? x, Individual? y)
            {
                return x.fitness.CompareTo(y.fitness);
            }
        }

        public static Individual TournamentSelection(Individual[] population, Random random)
        { 
            int index1 = random.Next(population.Length);
            int index2 = random.Next(population.Length);

            while(index1 == index2)
                index2 = random.Next(population.Length);

            return population[index1].fitness < population[index2].fitness ? population[index1] : population[index2];

        }



    }
}
