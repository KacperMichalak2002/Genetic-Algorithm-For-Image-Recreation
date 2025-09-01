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

        public static Individual TournamentSelection(List<Individual> population)
        {
            Random random = new Random();

            int index1 = random.Next(population.Count);
            int index2 = random.Next(population.Count);

            while(index1 == index2)
                index2 = random.Next(population.Count);

            return population[index1].fitness < population[index2].fitness ? population[index1] : population[index2];

        }



    }
}
