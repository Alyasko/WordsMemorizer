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
            _randomWordIndex = -1;

            NextWord();
        }

        private bool Check(int selectedIndex)
        {
            bool result = true;
            String selectedWordText = lbChoices.Items[selectedIndex].ToString();
            ForeignWord foundWord = _randomWords.FirstOrDefault(x =>
                x.Translation.Equals(selectedWordText) || x.IndefiniteForm.Equals(selectedWordText)
                || x.SecondForm.Equals(selectedWordText)
            );

            if (foundWord != _randomWords[_randomWordIndex])
            {
                result = false;
                MessageBox.Show("You have selected wrong word!\nPlease, try again!", "Information", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                result = true;
            }

            return result;
        }

        private void NextWord()
        {
            if (_randomWordIndex >= _randomWords.Count - 1)
            {
                End();
            }
            else
            {
                _randomWordIndex++;
                ForeignWord currentForeignWord = _randomWords[_randomWordIndex];

                tbSourceWord.Text = currentForeignWord.IndefiniteForm;

                List<String> choicesWords = _wordsMemorizerProcessor.Words.Select(x => x.Translation).ToList();
                lbChoices.Items.Clear();
                lbChoices.Items.AddRange(choicesWords.ToArray());

            }
        }

        private void End()
        {
            DialogResult result = MessageBox.Show("All words are checked.\nWhould you like to start again?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Start();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void lbChoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = lbChoices.SelectedIndex;
            if (selectedIndex != -1)
            {
                bool isCorrect = Check(selectedIndex);
                if (isCorrect)
                {
                    NextWord();
                }
            }
        }
    }
}
