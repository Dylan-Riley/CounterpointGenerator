using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public class Output: IOutput
    {
        public Output()
        {
        }

        public List<int> Cantus { get; set; }

        public override string ToString()
        {
            return string.Join(", ", Cantus);
        }
    }
}
