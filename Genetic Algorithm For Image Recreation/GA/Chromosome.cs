using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_For_Image_Recreation.GA
{
    internal class Chromosome
    {
        public String value { get; set; }
        private const int _maxLength = 4;
        public Chromosome() { 
            value = RandomString();
        }

        public String RandomString()
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            Char[] randomChars = new Char[_maxLength];

            for(int i = 0; i < _maxLength; i++)
            {
                randomChars[i] = chars[random.Next(chars.Length)];
            }

            String finalString = new String(randomChars);
            return finalString;
        }
    }
}
