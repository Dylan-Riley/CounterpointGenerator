using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    public class WeightSelect: IWeightSelect
    {
        Random _rnd = new Random();
        RuleInput ri = new RuleInput();

        public WeightSelect(RuleInput ri)
        {
            // TODO: Add handling of user input
            this.ri = ri;
        }

        public List<Note> SelectPossibilities()
        {
            // 80% of the time pick an imperfect consonance 
            IRules perfectOrImperfect = PickBetweenTwo(new ImperfectConsonanceRule(), 80,
                                                        new PerfectConsonanceRule());
            return perfectOrImperfect.Apply(this.ri);
        }

        private IRules PickBetweenTwo(IRules ruleA, int ruleAChance, IRules ruleB)
        {
            // Given two rules and the chance for one (out of 100), pick one at random
            int pickChance = _rnd.Next(1, 101);
            if (pickChance <= ruleAChance)
            {
                return ruleA;
            }
            else
            {
                return ruleB;
            }
        }
    }
}
