using System;
using System.Collections.Generic;

namespace CounterpointGenerator {
    public class PerfectConsonanceRule: IRules {
        /**
         * Perfect consonance includes the intervals fourth, fifth, octave
         * These are 5, 7, and 12 semitones apart respectively
         * Therefore, if currentNote is 5, 7, or 12 notes away
         * from a note it is a perfect consonance
         *
         * This also holds true for both up and down, only
         * down the fifth and fourth are reversed
         */

         //Includes one octave up or down
         // {8vb, lower fifth, lower fourth, upper fourth, upper fifth, 8va}
        private List<int> perfectIntervals = new List<int>(){-12, -7, -5, 5, 7, 12};

        /**
         * INPUTS: Possibilities, CurrentNote
         */
        public List<Note> Apply(RuleInput ruleInput) {
            List<Note> output = new List<Note>();
            foreach (Note n in ruleInput.Possibilities) {
                // Add checking for perfect fourth/fifth outside one octave difference?
                if (perfectIntervals.Contains(n.Pitch - ruleInput.CurrentNote.Pitch)){
                    //If the difference between note in possibilities n is
                    //equal to a perfect interval
                    output.Add(n);
                }
            }
            return output;
        }
    }
}