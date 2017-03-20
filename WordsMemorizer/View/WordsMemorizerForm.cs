using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordsMemorizer;

namespace View
{
    public partial class WordsMemorizerForm : Form
    {
        private WordsMemorizerProcessor _wordsMemorizerProcessor;
        private IList<ForeignWord> _randomWords;
        private int _randomWordIndex;

        public WordsMemorizerForm()
        {
            InitializeComponent();

            _wordsMemorizerProcessor = new WordsMemorizerProcessor();
        }

        private void WordsMemorizer_Load(object sender, EventArgs e)
        {
            _wordsMemorizerProcessor.InputWordsFileName = "input.txt";
            _wordsMemorizerProcessor.LoadWords();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            _randomWords = _wordsMemorizerProcessor.GetRandomWords(10);
            _randomWordIndex = 0;

            NextWord();
        }

        private void Check()
        {
            
        }

        private void NextWord()
        {
            ForeignWord currentForeignWord = _randomWords[_randomWordIndex];

            tbSourceWord.Text = currentForeignWord.IndefiniteForm;

            List<String> choicesWords = _randomWords.Select(x => x.Translation).ToList();
            lbChoices.Items.Clear();
            lbChoices.Items.AddRange(choicesWords.ToArray());

            _randomWordIndex++;
        }

        private void lbChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check();
            NextWord();
        }
    }
}
