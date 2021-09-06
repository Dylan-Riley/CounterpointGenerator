using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterpointGenerator
{
    class HeldNoteIntervalsRule: IRules
    {
        /*
         * If a counterpoint note would be held over multiple cantus firmus notes
         * make sure it's a legal interval for all cantus firmus notes
         */
        private readonly List<int> legalIntervals = Constants.IMPERFECT_INTERVALS.Concat(
                                                    Constants.PERFECT_INTERVALS).ToList();

        public List<Note> Apply(RuleInput ruleInput)
        {
            /*
             * TODO: There is a non-zero chance some of the following logic will be needed
             * again for other functions and should be scooped out and moved to MelodyLine
             * as public functions
             */

            List<Note> output = new List<Note>();
            foreach(Note n in ruleInput.Possibilities)
            {
                // NOTE: new note in possibilities can have started /after/ the "current" cantus note
                double currentNoteStart = ruleInput.CantusFirmus.GetBeatCountForNoteStart(ruleInput.CurrentNote);
                double relativeCurrentBeatCount = ruleInput.CurrentBeatCount - currentNoteStart;
                double untilNextNote = ruleInput.CurrentNote.Length - relativeCurrentBeatCount;
                // Some of that couldda been in the if statement but I needed it to be clearer
                if(n.Length <= untilNextNote)
                {
                    // If the possible note won't leak into the next note it should already be a legal interval
                    output.Add(n);
                }
                else
                {
                    List<Note> checkNotes = new List<Note>();
                    double addUp = n.Length;
                    bool firstNote = true;
                    while(addUp > 0)
                    {
                        // Accrue the required next notes
                        double addNoteCount = ruleInput.CurrentBeatCount + (n.Length - addUp);
                        Note addNote = ruleInput.CantusFirmus.GetNoteAtBeatCount(addNoteCount);
                        checkNotes.Add(addNote);
                        if (firstNote)
                        {
                            addUp -= untilNextNote;
                            firstNote = false;
                        }
                        else
                        {
                            addUp -= addNote.Length;
                        }
                    }

                    bool possibleNote = true;
                    foreach(Note check in checkNotes)
                    {
                        // Check possibility pitch against each of the next notes' pitches for intervals
                        if(!legalIntervals.Contains(n.Pitch - check.Pitch))
                        {
                            possibleNote = false;
                            // Optimization, stop bothering if mismatch found
                            break;
                        }
                    }
                    if (possibleNote)
                    {
                        output.Add(n);
                    }
                }
            }
            return output;
        }
    }
}
