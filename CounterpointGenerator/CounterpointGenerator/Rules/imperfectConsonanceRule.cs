using System;
using System.Collections.Generic;

namespace CounterpointGenerator {
    public class ImperfectConsonanceRule: IRules {
        /**
         * Imperfect consonance includes second, major and minor third,
         * major and minor sixth, and seventh.
         * HOWEVER
         * The text also forbids augmented, diminished, and chromatic intervals
         * To save some hassle I'm just gonna bake that into here too
         *
         * 3, 4, 8, and 9 semitones for m/M 3rd, m/M 6th
         */

        //Includes one octave up or down
        private readonly List<int> imperfectIntervals =  new List<int>(Constants.IMPERFECT_INTERVALS);

        /**
         * INPUTS: Possibilities, CurrentNote
         */
        public List<Note> Apply(RuleInput ruleInput){
            List<Note> output = new List<Note>();
            foreach (Note n in ruleInput.Possibilities) {
                // Add checking outside one octave range?
                if (imperfectIntervals.Contains(n.Pitch - ruleInput.CurrentNote.Pitch)){
                    //If the difference between not in possibilities n is equal to an imperfect interval
                    output.Add(n);
                }
            }
            return output;
        }
    }
}