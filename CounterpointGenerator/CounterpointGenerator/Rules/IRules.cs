namespace CounterpointGenerator {

    /**
     * Base interface for rules objects
     */
    public interface IRules {
        public List<int> apply(List<int> possibilities);
    }

}