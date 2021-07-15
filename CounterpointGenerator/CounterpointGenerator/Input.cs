using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * An implementation of the input interface.
     */
    public class Input: IInput
    {
        
        public Input(List<int> cantus)
        {
            Cantus = cantus;
        }

        /**
         * Cantus firmus.
         */
        public List<int> Cantus { get; set; }

        public override string ToString()
        {
            return string.Join(", ", Cantus);
        }
    }
}
