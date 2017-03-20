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
        private Random _random;

        public WordsMemorizerForm()
        {
            InitializeComponent();

            _wordsMemorizerProcessor = new WordsMemorizerProcessor();
            _random = new Random();
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
               selectedWordText.Contains(x.Translation) || selectedWordText.Contains(x.IndefiniteForm)
                || selectedWordText.Contains(x.SecondForm)
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
                _randomWords.RemoveAt(selectedIndex);
            }

            return result;
        }

        private void NextWord()
        {
            if (_randomWords.Count == 0)
            {
                End();
            }
            else
            {
                _randomWordIndex = _random.Next(0, _randomWords.Count - 1);
                ForeignWord currentForeignWord = _randomWords[_randomWordIndex];

                bool translationFirst = false;

                lbChoices.Items.Clear();

                if (translationFirst)
                {
                    tbSourceWord.Text = currentForeignWord.Translation;
                    lbChoices.Items.AddRange(
                        _randomWords.Select(x => $"{x.IndefiniteForm}, {x.SecondForm}, {x.ThirdForm}").ToArray());

                }
                else
                {
                    tbSourceWord.Text = $"{currentForeignWord.IndefiniteForm}, {currentForeignWord.SecondForm}, {currentForeignWord.ThirdForm}";
                    lbChoices.Items.AddRange(
                        _randomWords.Select(x => x.Translation).ToArray());
                }



            }
        }

        private void End()
        {
            Start();

            /*DialogResult result = MessageBox.Show("All words are checked.\nWhould you like to start again?", "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Start();
            }
            else if (result == DialogResult.No)
            {

            }*/
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
