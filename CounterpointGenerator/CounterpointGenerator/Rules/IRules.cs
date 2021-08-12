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
        public Note CurrentNote { get; set; }
        // Current position in generation, current count of notes
        // 0-indexed!
        public int Position { get; set; }
        // Total expected length of generation
        // 0-indexed, too!
        public int EndOn { get; set; }
        // Previous cantus note
        public Note PreviousCantus { get; set; }
        //Previous counterpoint note
        public Note PreviousCounterpoint { get; set; }

        public RuleInput()
        {
            // Empty constructor
        }
    }

}