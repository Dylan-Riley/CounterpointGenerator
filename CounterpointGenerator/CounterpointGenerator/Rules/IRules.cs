using System;
using System.Collections.Generic;

namespace CounterpointGenerator {

    /**
     * Base interface for rules objects
     */
    public interface IRules
    {
        public List<int> Apply(RuleInput ruleInput);
    }

    public class RuleInput
    {
        // Range of possible notes
        public List<int> Possibilities { get; set; }
        // Current cantus note in generation
        public int? CurrentNote { get; set; } = null;
        // Current position in generation, current count of notes
        public int? Position { get; set; } = null;
        // Total expected length of generation
        public int? Length { get; set; } = null;
        // Previous cantus note
        public int? PreviousCantus { get; set; } = null;
        //Previous counterpoint note
        public int? PreviousCounterpoint { get; set; } = null;
    }

}