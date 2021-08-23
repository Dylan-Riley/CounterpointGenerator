using System;
using System.Collections.Generic;

namespace CounterpointGenerator {

    /**
     * Base interface for rules objects
     */
    public interface IRules
    {
        public List<Note> Apply(RuleInput ruleInput);
    }

    public class RuleInput
    {
        // Range of possible notes
        public List<Note> Possibilities { get; set; }

        // Current cantus note in generation
        public Note CurrentNote { get; set; } = null;

        // Next note to check
        public Note NextNote { get; set; } = null;

        // Duration of new note to add
        public double NewNoteLength { get; set; }

        // Current note in cantus firmus
        // 0-indexed!
        public int Position { get; set; }

        // Total number of notes in cantus firmus
        // 0-indexed, too!
        public int EndOn { get; set; }

        // Previous cantus note
        public Note PreviousCantus { get; set; } = null;

        //Previous counterpoint note
        public Note PreviousCounterpoint { get; set; } = null;


        public RuleInput()
        {
            // Empty constructor
        }
    }

}