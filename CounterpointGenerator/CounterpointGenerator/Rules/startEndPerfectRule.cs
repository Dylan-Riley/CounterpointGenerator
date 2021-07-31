using System;
using System.Collections.Generic;

namespace CounterpointGenerator{
    public class StartEndPerfectRule: IRules {
        /**
         * The first and last notes of the counterpoint must be a perfect consonance
         */
        
        /**
         * INPUTS: Possibilities, CurrentNote, Position, Length
         */
        public List<Note> Apply(RuleInput ruleInput){
            if (ruleInput.Position == 0 || ruleInput.Position == ruleInput.Length){
                // If we're in the first or last note
                PerfectConsonanceRule reuseCode = new PerfectConsonanceRule();
                return reuseCode.Apply(ruleInput);
            }
            else {
                return ruleInput.Possibilities;
            }
        }
    }
}