using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * A mocked implementation of the output translator layer.
     */
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
