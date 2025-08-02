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
        public Fitness fitness { get; set; }

        public Individual(){ }
    }
}
