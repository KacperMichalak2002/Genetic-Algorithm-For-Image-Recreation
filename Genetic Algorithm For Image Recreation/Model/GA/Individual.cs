using Genetic_Algorithm_For_Image_Recreation.Model.Processing;

namespace Genetic_Algorithm_For_Image_Recreation.Model.GA
{
    internal class Individual
    {
       public Chromosome Chromosome { get; set; }
       public double fitness { get; set; }
       public PixelColor[]? pixels { get; set; }


        public Individual() { }

        public Individual(Chromosome chromosome)
        {
            Chromosome = chromosome;
            fitness = double.MaxValue;
            pixels = null;
        }

        public Individual Clone()
        {
            Individual newIndividual = new Individual
            {
                Chromosome = Chromosome.Clone(),
                fitness = fitness,
                pixels = pixels
            
            };
            return newIndividual;
        }

    }
}
