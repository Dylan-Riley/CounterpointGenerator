namespace CounterpointGenerator{
    public class penultimateNoteRule: IRules {
        /**
         * The second-to-last bar has strict requirements
         * For a note lower than the cantus firmus it must be a major sixth
         * For a note higher than the cantus firmus it must be a minor third
         */
        
        private Set<int> legalNotes = {-3, 3};

        public List<int> apply(List<int> possibilities, int currentNote, int position, int length) {
            if (length - position == 1){
                // If we're in the penultimate note
                List<int> output = new List<int>();
                foreach (int n in possibilities) {
                    if(legalNotes.Contains(n - currentNote)){
                        output.add(n);
                    }
                }
                return output;
            }
            else {
                return possibilities;
            }
        }
    }
}