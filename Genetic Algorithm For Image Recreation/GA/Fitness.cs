using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Fitness
    {
        
        private String TARGET = "Kacper";

        public int CalculateFitness(Individual individual)
        {
            int value = 0;

            Chromosome invChromosome = individual.chromosome;
            int size = invChromosome.value.Length;

            for (int i = 0; i < size; i++)
            {
                if (invChromosome.value[i] != TARGET[i])
                {
                    value++;
                }
            }

            return value;
        }
    }
}
