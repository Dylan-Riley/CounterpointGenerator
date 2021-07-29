﻿using System;
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
        public List<Note> Apply(RuleInput ruleInput)
        {
            // Do nothing if no previous notes!
            if (ruleInput.Position == 0)
            {
                return ruleInput.Possibilities;
            }

            List<Note> output = new List<Note>(ruleInput.Possibilities);
            // Check if previous cantus stepped up
            if (ruleInput.PreviousCantus.Pitch < ruleInput.CurrentNote.Pitch)
            {
                // Check if previous counterpoint was the higher voice
                if(ruleInput.PreviousCounterpoint.Pitch > ruleInput.PreviousCantus.Pitch)
                {
                    // Remove upper octave as a possibility
                    Note removeNote = new Note(ruleInput.CurrentNote.Pitch + 13, ruleInput.CurrentNote.Length);
                    // Remove all items n such that n's pitch is equal to the removeNote pitch
                    output.RemoveAll(n => n.Pitch == removeNote.Pitch);
                }
            }
            // Check if previous cantus stepped down
            else if (ruleInput.PreviousCantus.Pitch > ruleInput.CurrentNote.Pitch)
            {
                // Check if previous counterpoint was the lower voice
                if(ruleInput.PreviousCounterpoint.Pitch < ruleInput.PreviousCantus.Pitch)
                {
                    // Remove lower octave as a possibility
                    Note removeNote = new Note(ruleInput.CurrentNote.Pitch - 13, ruleInput.CurrentNote.Length);
                    // Remove all items n such that n's pitch is equal to the removeNote pitch
                    output.RemoveAll(n => n.Pitch == removeNote.Pitch);
                }
            }
            return output;
        }
    }
}
