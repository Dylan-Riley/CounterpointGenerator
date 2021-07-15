using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public class Input: IInput
    {
        
        public Input(List<int> cantus)
        {
            Cantus = cantus;
        }

        public List<int> Cantus { get; set; }

        public override string ToString()
        {
            return string.Join(", ", Cantus);
        }
    }
}
