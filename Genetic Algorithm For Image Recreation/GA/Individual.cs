using Genetic_Algorithm_For_Image_Recreation.Renderer;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Individual
    {
       public Chromosome Chromosome { get; set; }
       public double fitness { get; set; }
       public PixelColor[] pixels { get; set; }


        public Individual() { }

        public Individual(Chromosome chromosome)
        {
            Chromosome = chromosome;
            fitness = 0;
        }




        public Individual Clone()
        {
            Individual newIndividual = new Individual
            {
                Chromosome = this.Chromosome.Clone(),
                fitness = this.fitness,
                pixels = this.pixels
            
            };
            return newIndividual;
        }

    }
}
