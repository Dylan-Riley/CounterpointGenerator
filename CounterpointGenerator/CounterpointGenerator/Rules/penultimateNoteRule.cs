using System;
using System.Collections.Generic;

namespace CounterpointGenerator{
    public class PenultimateNoteRule: IRules {
        /**
         * The second-to-last bar has strict requirements
         * For a note lower than the cantus firmus it must be a major sixth
         * For a note higher than the cantus firmus it must be a minor third
         * This means the legal note is either three semitones up or three semitones down
         */
        private readonly List<int> legalIntervals = new List<int>()
        {
            Constants.LOWER_MAJOR_SIXTH,
            Constants.UPPER_MINOR_THIRD
        };

        /**
         * INPUTS: Possibilities, CurrentNote, Position, Length
         */
        public List<Note> Apply(RuleInput ruleInput) {
            if (ruleInput.Length - ruleInput.Position == 1){
                // If we're in the penultimate note
                List<Note> output = new List<Note>();
                foreach (Note n in ruleInput.Possibilities) {
                    if(legalIntervals.Contains(n.Pitch - ruleInput.CurrentNote.Pitch)){
                        output.Add(n);
                    }
                }
                return output;
            }
            else {
                return ruleInput.Possibilities;
            }
        }
    }
}