using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpointGenerator
{
    class Constants
    {
        // REFERENCE NOTE
        public static int C5 = 0;

        // TEST INPUT
        /*
         * |0000|0000|0000|0000||1111|2222|3333|3333||4466|0000|0000|2222||
         */
        public static string TEST_INPUT = "0 16, 1 4, 2 4, 3 8, 4 2, 6 2, 0 8, 2 4";

        // NOTE LENGTHS
        /*
         * Sixteenth notes are a good reasonable "smallest unit"
         * with lower double values available for even smaller if needed
         * Adjust WHOLE_NOTE_LENGTH to change math for all lengths
         */
        public static double WHOLE_NOTE_LENGTH = 16.0;
        public static double HALF_NOTE_LENGTH = WHOLE_NOTE_LENGTH / 2;
        public static double QUARTER_NOTE_LENGTH = HALF_NOTE_LENGTH / 2;
        public static double EIGHTH_NOTE_LENGTH = QUARTER_NOTE_LENGTH / 2;
        public static double SIXTEENTH_NOTE_LENGTH = EIGHTH_NOTE_LENGTH / 2;

            // Adjust this to change some rule behavior
        public static double EXPECTED_LONGEST_NOTE = WHOLE_NOTE_LENGTH;

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

        public static readonly List<int> PERFECT_INTERVALS = new List<int>()
        {
            OCTAVE_DOWN,
            LOWER_FIFTH,
            LOWER_FOURTH,
            UPPER_FOURTH,
            UPPER_FIFTH,
            OCTAVE
        };

        // IMPERFECT
        public static int UPPER_MINOR_THIRD = 3;
        public static int LOWER_MINOR_THIRD = -9;
        public static int UPPER_MAJOR_THIRD = 4;
        public static int LOWER_MAJOR_THIRD = -8;
        public static int UPPER_MINOR_SIXTH = 8;
        public static int LOWER_MINOR_SIXTH = -4;
        public static int UPPER_MAJOR_SIXTH = 9;
        public static int LOWER_MAJOR_SIXTH = -3;

        public static readonly List<int> IMPERFECT_INTERVALS = new List<int>()
        {
            LOWER_MINOR_THIRD,
            LOWER_MAJOR_THIRD,
            LOWER_MINOR_SIXTH,
            LOWER_MAJOR_SIXTH,
            UPPER_MINOR_THIRD,
            UPPER_MAJOR_THIRD,
            UPPER_MINOR_SIXTH,
            UPPER_MAJOR_SIXTH
        };

        // Default max time to generate
        // Moved from Input.cs
        public static double DEFAULT_TIME_LIMIT = 10.0;

        // TIME SIGNATURE KEYS
        public static string TIMESIG_BEATS = "beats";
        public static string TIMESIG_VALUE = "value";

        // Default Time Signature numbers
        public static int DEFAULT_MEASURE_BEATS = 4;
        public static int DEFAULT_BEAT_VALUE = 4;

        // Number of initial random notes to generate before rules
        public static int GIVE_ME_LOTS_OF_NOTES = 200;

        // % error allowed in output collecting
        // for considered "close to" completed
        public static double OUTPUT_ERROR_MARGIN = 10.0;
        // DEFAULT "10.0" means if a melody line is 90% of the way
        // to complete it is "close to"
    }
}
