using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public class RuleApplier: Rules.IRuleApplier
    {
        private List<IRules> ruleSet;

        public RuleApplier()
        {
            ruleSet = new List<IRules>();
        }

        public Boolean GenerateSet()
        {
            // Build ruleset
            // Non-weighted rules, weighted chance rules to be handled elsewhere
            ruleSet.Add(new StartEndPerfectRule());
            ruleSet.Add(new PenultimateNoteRule());
            ruleSet.Add(new BattutaRule());

            // Return true if all rules added
            return ruleSet.Count == 3;
        }

        public void Applicator (RuleInput ruleInput)
        {
            // Mutate ruleInput.Possibilities, result and logic will need testing
            foreach (IRules r in ruleSet)
            {
                ruleInput.Possibilities = r.Apply(ruleInput);
            }
        }
    }
}
