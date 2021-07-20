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
        private readonly List<int> legalNotes = new List<int>(){-3, 3};

        /**
         * INPUTS: Possibilities, CurrentNote, Position, Length
         */
        public List<int> Apply(RuleInput ruleInput) {
            if (ruleInput.Length - ruleInput.Position == 1){
                // If we're in the penultimate note
                List<int> output = new List<int>();
                foreach (int n in ruleInput.Possibilities) {
                    if(legalNotes.Contains(n - ruleInput.CurrentNote)){
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