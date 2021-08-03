using System;
using System.Collections.Generic;

namespace CounterpointGenerator.Rules
{
    public interface IRuleApplier
    {
        public Boolean GenerateSet();

        public void Applicator(RuleInput ruleInput);
    }
}
