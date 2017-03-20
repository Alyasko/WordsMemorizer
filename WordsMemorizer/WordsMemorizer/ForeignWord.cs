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
            ThirdForm = "";
            Translation = "";
        }

        public ForeignWord(String indefiniteForm, String secondForm, String thirdForm, String translation)
        {
            IndefiniteForm = indefiniteForm;
            SecondForm = secondForm;
            ThirdForm = thirdForm;
            Translation = translation;
        }

        public override string ToString()
        {
            return IndefiniteForm;
        }

        public String ThirdForm { get; set; }
        public String IndefiniteForm { get; set; }
        public String SecondForm { get; set; }
        public String Translation { get; set; }
    }
}
