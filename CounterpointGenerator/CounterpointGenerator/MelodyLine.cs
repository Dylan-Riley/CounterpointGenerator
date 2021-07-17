using System;
namespace CounterpointGenerator
{
    public class MelodyLine
    {
        public Note FirstNote { get; set; }

        public MelodyLine RemoveFirstNote()
        {
            throw new NotImplementedException();
        }

        /**
         * Takes in a note.
         * Prepends it to the melody line (making it hte new first note)
         * Returns the new melodyLine.
         */
        public MelodyLine Prepend(Note firstNote)
        {
            throw new NotImplementedException();
        }

        public MelodyLine()
        {
        }
    }
}
