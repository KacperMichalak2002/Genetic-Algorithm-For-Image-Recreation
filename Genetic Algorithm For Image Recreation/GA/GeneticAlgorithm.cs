using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class GeneticAlgorithm
    {
        private class FitnessComparer : IComparer<Individual>
        {
            public int Compare(Individual? x, Individual? y)
            {
                return x.fitness.CompareTo(y.fitness);
            }
        }

        private List<Individual> _individuals;
        private const int POPULATION_SIZE = 20;
        private Fitness _fitness = new Fitness();
        

        private void InitializePopulation()
        {
            _individuals = new List<Individual>();

            for (int i = 0; i < POPULATION_SIZE; i++) {
                _individuals.Add(new Individual());
            }
        }

        private Individual TournamentSelection(List<Individual> individuals)
        {
            Random random = new Random();

            int index1 = random.Next(POPULATION_SIZE);
            int index2 = random.Next(POPULATION_SIZE);
            while (index1 == index2)
            {
                index2 = random.Next(POPULATION_SIZE);
            }

            return individuals[index1].fitness < individuals[index2].fitness ? individuals[index1] : individuals[index2];
            
        }

        private Individual Crossover(Individual parent1, Individual parent2)
        {

            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            double mutationProbability = 0.3f;
            int len = parent1.chromosome.value.Length;
            StringBuilder childChromosome = new StringBuilder();
            Individual child = new Individual();

            for(int i = 0; i < len; i++)
            {
                if(random.Next(100) < 50)
                {
                    childChromosome.Append(parent1.chromosome.value[i]);
                }
                else
                {
                    childChromosome.Append(parent2.chromosome.value[i]);
                }

                if(random.NextDouble() < mutationProbability)
                {
                    int geneLen = chars.Length;
                    int r = random.Next(0, geneLen);
                    childChromosome[i] = chars[r];
                }
                
                
            }

            return new Individual(new Chromosome(childChromosome.ToString()));

        }

        public void Start(TextBlock txtBlock1)
        {
            InitializePopulation();
            Boolean found = false;
            Random random = new Random();
            double crossoverProbability = 0.7f;
            StringBuilder sbInfo = new StringBuilder();
            int generation = 0;

            while(!found)
            {
                if (found)
                    break;
                for (int i = 0; i < POPULATION_SIZE; i++)
                {
                    Individual currentIndividual = _individuals[i];
                    currentIndividual.fitness = _fitness.CalculateFitness(currentIndividual);
                    if (currentIndividual.fitness.Equals(0))
                    {
                        found = true;
                        sbInfo.Append($"{i + 1}. F: {currentIndividual.fitness} gen {generation} ");
                        sbInfo.AppendLine(currentIndividual.chromosome.value);
                        break;
                    }
                    sbInfo.Append($"{i + 1}. F: {currentIndividual.fitness} ");
                    sbInfo.AppendLine(currentIndividual.chromosome.value);

                }
                txtBlock1.Text = sbInfo.ToString();

                _individuals.Sort(new FitnessComparer());

                
                List<Individual> newPopulation = new List<Individual>();


                int elit = (10 * POPULATION_SIZE) / 100;

                for(int j =0; j < elit; j++)
                    newPopulation.Add(_individuals[j]);

                while (newPopulation.Count < _individuals.Count)
                {
                    Individual parent1 = TournamentSelection(_individuals);
                    Individual parent2 = TournamentSelection(_individuals);

                    while (parent1 == parent2)
                    {
                        parent2 = TournamentSelection(_individuals);
                    }

                    if (random.NextDouble() < crossoverProbability)
                    {
                        Individual child = Crossover(parent1, parent2);

                        newPopulation.Add(child);
                    }
                    
                }
                _individuals = newPopulation;
                sbInfo.Clear();
                generation++;
            }
           

        }
    }
}
