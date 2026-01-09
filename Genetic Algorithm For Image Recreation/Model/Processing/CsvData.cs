using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.Model.Processing
{
    class CsvData
    {
        public int generationId { get; set; }
        public double bestFitness { get; set; }
        public int geneCount { get; set; }
        public double timeElapsed { get; set; }

    }
}
