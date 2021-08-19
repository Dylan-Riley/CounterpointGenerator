using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * An implementation of the input interface.
     */
    public class Input: IInput
    {
        public static double DEFAULT_PREFERENCE = 10;
        public Input(MelodyLine cantus, double preference)
        {
            Cantus = cantus;
            userPreference = preference;
        }

        /**
         * Cantus firmus.
         */
        public MelodyLine Cantus { get; set; }
        public double userPreference { get; set; }

        public override string ToString()
        {
            return Cantus.ToString();   
        }
    }
}
