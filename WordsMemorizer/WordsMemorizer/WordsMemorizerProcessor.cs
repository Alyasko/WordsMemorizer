using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsMemorizer
{
    public class WordsMemorizerProcessor
    {
        private Random _random;

        public WordsMemorizerProcessor()
        {
            Words = new List<ForeignWord>();
            _random = new Random();
        }

        /// <summary>
        /// Loads words from specified file.
        /// </summary>
        public void LoadWords()
        {
            String[] lines = File.ReadAllLines(InputWordsFileName);

            Words.Clear();

            foreach (string line in lines)
            {
                String[] words = line.Split(new String[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
                ForeignWord foreignWord = new ForeignWord(words[0], words[3], words[1]);
                Words.Add(foreignWord);
            }
        }

        public IList<ForeignWord> GetRandomWords(int count)
        {
            List<ForeignWord> randomWords = new List<ForeignWord>();
            List<ForeignWord> foreignWordsSource = new List<ForeignWord>();

            foreignWordsSource.AddRange(Words);

            if (count > foreignWordsSource.Count)
            {
                count = foreignWordsSource.Count;
            }

            for (int i = 0; i < count; i++)
            {
                int randomIndex = _random.Next(0, foreignWordsSource.Count);
                randomWords.Add(foreignWordsSource[randomIndex]);
                foreignWordsSource.RemoveAt(randomIndex);
            }

            return randomWords;
        }

        public IList<ForeignWord> Words { get; set; }

        public String InputWordsFileName { get; set; }
    }
}
