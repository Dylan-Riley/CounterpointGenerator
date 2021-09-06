using System;
using System.Collections.Generic;
using System.Text;

namespace CounterpointGenerator
{
    class RuleApplierBeats : Rules.IRuleApplier
    {
        private List<IRules> ruleSet;

        public RuleApplierBeats()
        {
            ruleSet = new List<IRules>();
        }

        public Boolean GenerateSet()
        {
            ruleSet.Add(new NoteTooLongRule()); // NEEDS TO BE FIRST
            ruleSet.Add(new HeldNoteIntervalsRule());

            return ruleSet.Count == 2;
        }

        public void Applicator (RuleInput ri)
        {
            foreach(IRules r in ruleSet)
            {
                ri.Possibilities = r.Apply(ri);
            }
        }
    }
}
