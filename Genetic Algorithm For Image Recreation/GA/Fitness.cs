using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Fitness
    {
        public int value;
        private String TARGET = "Kacper";

        public Fitness()
        {
            value = 0;
        }
        public int CalculateFitness(Individual individual)
        {
            Chromosome invChromosome = individual.chromosome;
            int size = invChromosome.value.Length;

            foreach (char ch in invChromosome.value) {
                value += TARGET.Contains(ch) ? 1 : 0;
            }

            return value;
        }
    }
}
