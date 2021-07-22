using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    class RuleApplier
    {
        private List<IRules> ruleSet = new List<IRules>();

        public Boolean generateSet()
        {
            // Build ruleset
            ruleSet.Add(new StartEndPerfectRule());
            ruleSet.Add(new PenultimateNoteRule());
            ruleSet.Add(new BattutaRule());

            // Return true if all rules added
            if (ruleSet.Count == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Applicator (RuleInput ruleInput)
        {
            foreach (IRules r in ruleSet)
            {
                ruleInput.Possibilities = r.Apply(ruleInput);
            }
        }
    }
}
