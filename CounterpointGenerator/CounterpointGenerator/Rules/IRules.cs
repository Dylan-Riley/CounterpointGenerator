using System;
using System.Collections.Generic;

namespace CounterpointGenerator {

    /**
     * Base interface for rules objects
     */
    public interface IRules {
        public List<int> Apply(List<int> possibilities, int currentNote);
    }

}