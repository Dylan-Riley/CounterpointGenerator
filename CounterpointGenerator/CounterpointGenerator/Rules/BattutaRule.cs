using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    class BattutaRule : IRules
    {
        /**
         * An octave cannot be met by stepping down in the higher voice and up in the lower voice
         * This is called a battuta in Italian
         */

        /**
         * INPUTS: Possibilities, CurrentNote, PreviousCantus, PreviousCounterpoint
         */
        public List<int> Apply(RuleInput ruleInput)
        {
            // Do nothing if no previous notes!
            if (ruleInput.Position == 0)
            {
                return ruleInput.Possibilities;
            }

            List<int> output = new List<int>(ruleInput.Possibilities);
            // Check if previous cantus stepped up
            if (ruleInput.PreviousCantus < ruleInput.CurrentNote)
            {
                // Check if previous counterpoint was the higher voice
                if(ruleInput.PreviousCounterpoint > ruleInput.PreviousCantus)
                {
                    // Remove upper octave as a possibility
                    output.Remove(ruleInput.CurrentNote + 13);
                }
            }
            // Check if previous cantus stepped down
            else if (ruleInput.PreviousCantus > ruleInput.CurrentNote)
            {
                // Check if previous counterpoint was the lower voice
                if(ruleInput.PreviousCounterpoint < ruleInput.PreviousCantus)
                {
                    // Remove lower octave as a possibility
                    output.Remove(ruleInput.CurrentNote - 13);
                }
            }
            return output;
        }
    }
}
