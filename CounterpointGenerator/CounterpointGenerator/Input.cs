using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * An implementation of the input interface.
     */
    public class Input: IInput
    {
        
        public Input(MelodyLine cantus)
        {
            Cantus = cantus;
        }

        /**
         * Cantus firmus.
         */
        public MelodyLine Cantus { get; set; }

        public override string ToString()
        {
            return string.Join(", ", Cantus);
        }
    }
}
