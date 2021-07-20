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
        // Current note in generation
        public int CurrentNote { get; set; }
        // Current position in generation, current count of notes
        public int Position { get; set; }
        // Total expected length of generation
        public int Length { get; set; }
    }

}