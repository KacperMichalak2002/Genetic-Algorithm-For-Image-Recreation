using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        private List<Individual> _individuals;
        private const int POPULATION_SIZE = 5;

        private void InitializePopulation()
        {
            _individuals = new List<Individual>();

            for (int i = 0; i < POPULATION_SIZE; i++) {
                _individuals.Add(new Individual());
            }
        }

        public String getText()
        {
            return "TEST";
        }

        public void Start()
        {
            InitializePopulation();
        }
    }
}
