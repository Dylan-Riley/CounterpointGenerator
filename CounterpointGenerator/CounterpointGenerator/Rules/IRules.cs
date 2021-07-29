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
        public int Position { get; set; }
        // Total expected length of generation
        public int Length { get; set; }
        // Previous cantus note
        public Note PreviousCantus { get; set; }
        //Previous counterpoint note
        public Note PreviousCounterpoint { get; set; }
    }

}