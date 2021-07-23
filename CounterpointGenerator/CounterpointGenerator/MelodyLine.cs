using System;
using System.Collections.Generic;
namespace CounterpointGenerator
{
    public class MelodyLine
    {
        public List<Note> MelodyLine { get; set; }


        public Note FirstNote {
            get
            {
                return this.MelodyLine[0];
            }
        }

        public MelodyLine RemoveFirstNote()
        {
            return this.MelodyLine.RemoveAt(0);
        }

        /**
         * Takes in a note.
         * Prepends it to the melody line (making it the new first note)
         * Returns the new melodyLine.
         */

        public MelodyLine Prepend(Note firstCounterNote)
        {
            return this.MelodyLine.Insert(0, firstCounterNote);
        }

        public MelodyLine()
        {
            this.MelodyLine = new List<Note>();
        }
    }
            
        
        
}
