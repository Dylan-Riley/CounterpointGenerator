using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpointGenerator
{
    class SuccessionOfFifthsRule: IRules
    {
        /*
         * Two fifths cannot be interupted by a third
         * 
         * Best way I can think of to do this code-wise would just be to not
         * allow the counterpoint to generate a fifth, third, fifth pattern
         */

        public List<Note> Apply(RuleInput ruleInput)
        {
            List<Note> output = new List<Note>();
            foreach(Note n in ruleInput.Possibilities)
            {
                int currentInterval = n.Pitch - ruleInput.CurrentNote.Pitch;
                if (currentInterval == Constants.UPPER_FIFTH || currentInterval == Constants.LOWER_FIFTH
                    || !(ruleInput.CurrentBeatCount == 0))
                {
                    // If we want to add a fifth, start checking rule logic
                    Note previousCounterpoint = ruleInput.CounterpointThusFar.LastNote;
                    double previousBeat = ruleInput.CounterpointThusFar.GetBeatCountForNoteStart(previousCounterpoint);
                    Note previousCantus = ruleInput.CantusFirmus.GetNoteAtBeatCount(previousBeat);
                    int previousInterval = previousCounterpoint.Pitch - previousCantus.Pitch;
                    List<int> thirds = new List<int>() { Constants.UPPER_MAJOR_THIRD, Constants.UPPER_MINOR_THIRD, Constants.LOWER_MAJOR_THIRD, Constants.LOWER_MINOR_THIRD };

                    if (!thirds.Contains(previousInterval))
                    {
                        output.Add(n);
                    }
                }
                else
                {
                    // Also goes here if the beatcount is 0, if generating first note
                    output.Add(n);
                }
            }
            return output;
        }
    }
}
