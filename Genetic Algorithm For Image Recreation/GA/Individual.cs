namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Individual
    {
       public Chromosome Chromosome { get; set; }
       public double fitness { get; set; }

        public Individual(Chromosome chromosome)
        {
            Chromosome = chromosome;
            fitness = 0;
        }
    }
}
