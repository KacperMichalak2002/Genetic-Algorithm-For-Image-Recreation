using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Individual
    {
        public Chromosome chromosome { get; set; }
        public int fitness { get; set; }

        public Individual()
        {
            chromosome = new Chromosome();
            fitness = 0;
        }

        public Individual(Chromosome chromosome)
        {
            this.chromosome = chromosome;
        }
    }
}
