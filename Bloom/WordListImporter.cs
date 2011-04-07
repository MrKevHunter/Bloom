using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bloom
{
    public class WordListImporter
    {
        public static IEnumerable<string> GetWordList()
        {
            string path = @"D:\coding\Kata\Kata Five - Bloom\Bloom\Bloom\wordlist.txt";
            return File.ReadAllLines(path);
        }
    }

    public class BloomFilterList
    {
        public const int ARRAY_SIZE = 500000000;
        private bool[] instance;

        public string NumberOfBitsSet()
        {
            return string.Format("{0} of {1} Set", instance.Count(b => b), ARRAY_SIZE);
        }

        public void GetBloomList()
        {
            if (instance != null)
            {
                return;
            }
            instance = new bool[ARRAY_SIZE];
            foreach (string word in WordListImporter.GetWordList().AsParallel())
            {
                int calculateHash = new StringHashStrategy1().CalculateHash(word, ARRAY_SIZE);
                instance[calculateHash] = true;
            }
        }

        public bool CheckWord(string word)
        {
            if (instance == null)
            {
                GetBloomList();
            }
            int calculateHash = new StringHashStrategy1().CalculateHash(word, ARRAY_SIZE);
            return instance[calculateHash];
        }
    }

    public class StringHashStrategy1 : IStrategy
    {
        public int CalculateHash(string word, decimal arraySize)
        {
            return word.Aggregate(0, (current, c) => current ^ c);
        }
    }

    public class StringHashStrategy3 : IStrategy
    {
        #region IStrategy Members

        private readonly int[] _primes = new[]
                                   {
                                       2221, 6703, 5009, 5333, 7727, 7867, 1607, 1733, 2617, 3083, 4259, 4457, 4799, 4813,
                                       283, 839, 1117, 1373,1087,2083,3313,3779,4127,5227,3557,4723,5749,7879,7841,7573,3581
                                   };

        public int CalculateHash(string word, decimal arraySize)
        {
            return word.Select((t, i) => t*_primes[i]).Sum();
        }

        #endregion
    }

    public class StringHashStrategy2 : IStrategy
    {
        #region IStrategy Members

        public int CalculateHash(string word, decimal arraySize)
        {
            decimal result = word.Aggregate<char, decimal>(0, (current, c) => current + c*4409);

            result *= word.Length;
            Console.WriteLine(result);
            return (int) result;
        }

        #endregion
    }

    public interface IStrategy
    {
        int CalculateHash(string word, decimal arraySize);
    }
}

