namespace CounterpointGenerator{
    public class startEndPerfectRule: IRules {
        /**
         * The first and last notes of the counterpoint must be a perfect consonance
         */
        
        public List<int> apply(List<int> possibilities, int currentNote, int position, int length){
            if (position == 0 || position == length){
                // If we're in the first or last note
                return perfectConsonanceRule.apply(possibilities, currentNote);
            }
            else {
                return possibilities;
            }
        }
    }
}