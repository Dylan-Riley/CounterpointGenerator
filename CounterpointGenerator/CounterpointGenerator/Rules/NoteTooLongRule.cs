using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpointGenerator
{
    class NoteTooLongRule: IRules
    {
        /*
         * If a note in possibilities would result in the counterpoint
         * being longer (beat-count-wise) than the cantus firmus
         */
        public List<Note> Apply(RuleInput ruleInput)
        {
            if(ruleInput.ExpectedTotalBeatCount - ruleInput.CurrentBeatCount 
                      > Constants.EXPECTED_LONGEST_NOTE)
            {
                // Optimization, don't bother checking at all for early notes
                return ruleInput.Possibilities;
            }

            List<Note> output = new List<Note>();
            foreach(Note n in ruleInput.Possibilities)
            {
                if(ruleInput.CurrentBeatCount + n.Length <= ruleInput.ExpectedTotalBeatCount)
                {
                    // The new note plus the current beat count would not exceed the total beat count
                    output.Add(n);
                }
            }
            return output;
        }
    }
}
