using System;
using System.Collections.Generic;

namespace CounterpointGenerator {

    /**
     * Base interface for rules objects
     */
    public interface IRules {
        public List<int> Apply(List<int> possibilities, int currentNote);
    }

    //TODO:
    //Add IRuleInput interface with optional members that inherits this class
    //Then has optional members for the different inputs of Apply

}