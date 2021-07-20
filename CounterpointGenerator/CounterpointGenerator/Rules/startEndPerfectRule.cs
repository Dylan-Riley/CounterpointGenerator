using System;
using System.Collections.Generic;

namespace CounterpointGenerator{
    public class startEndPerfectRule: IRules {
        /**
         * The first and last notes of the counterpoint must be a perfect consonance
         */
        
        public List<int> Apply(List<int> possibilities, int currentNote, int position, int length){
            if (position == 0 || position == length){
                // If we're in the first or last note
                PerfectConsonanceRule reuseCode = new PerfectConsonanceRule();
                return reuseCode.Apply(possibilities, currentNote);
            }
            else {
                return possibilities;
            }
        }

        public List<int> Apply(List<int> possiblilities, int currentNote)
        {
            throw new ArgumentException("Start and End rule needs more input!");
        }
    }
}