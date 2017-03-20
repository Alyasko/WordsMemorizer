using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsMemorizer
{
    public class ForeignWord
    {
        public ForeignWord()
        {
            IndefiniteForm = "";
            SecondForm = "";
            Translation = "";
        }

        public ForeignWord(String indefiniteForm, String secondForm, String translation)
        {
            IndefiniteForm = indefiniteForm;
            SecondForm = secondForm;
            Translation = translation;
        }

        public override string ToString()
        {
            return IndefiniteForm;
        }

        public String IndefiniteForm { get; set; }
        public String SecondForm { get; set; }
        public String Translation { get; set; }
    }
}
