using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpointGenerator
{
    class Constants
    {
        // REFERENCE NOTE
        public static int C5 = 0;

        // INTERVALS
        /*
         * Counted around the idea that C5 = 0
         * Counted number of semitones between notes
         * For example C5 = 0, G5 = 7
         * Therefore a fifth = 7 - 0 = 7
         * 
         * A "lower" version of an interval is the same pitch,
         * but then lowered an octave
         * F5 is five semitones higher than C5
         * but F4 is seven semitones lower than C5
         */

        // PERFECT
        public static int OCTAVE = 12;
        public static int OCTAVE_DOWN = -12;
        public static int UPPER_FOURTH = 5;
        public static int LOWER_FOURTH = -7;
        public static int UPPER_FIFTH = 7;
        public static int LOWER_FIFTH = -5;

        // IMPERFECT
        public static int UPPER_MINOR_THIRD = 3;
        public static int LOWER_MINOR_THIRD = -9;
        public static int UPPER_MAJOR_THIRD = 4;
        public static int LOWER_MAJOR_THIRD = -8;
        public static int UPPER_MINOR_SIXTH = 8;
        public static int LOWER_MINOR_SIXTH = -4;
        public static int UPPER_MAJOR_SIXTH = 9;
        public static int LOWER_MAJOR_SIXTH = -3;
    }
}
