using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloom.Harness
{
    class Program
    {
        static void Main(string[] args)
        {
            var spellChecker = new BloomFilterList();
            bool hasWord = spellChecker.CheckWord("Hellenizations");
            if (hasWord)
            {
                Console.WriteLine("Word is found");

            }
            else
            {
                Console.WriteLine("word is NOT found");
            }
            Console.WriteLine(spellChecker.NumberOfBitsSet());
            Console.ReadLine();
        }
    }
}
