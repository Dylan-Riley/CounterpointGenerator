using System;
using System.Collections.Generic;
namespace CounterpointGenerator
{
    public class MelodyLine
    {
        public List<Note> AMelodyLine { get; set; }


        public Note FirstNote {
            get
            {
                return this.AMelodyLine[0];
            }
        }

        public void RemoveFirstNote()
        {
            this.AMelodyLine.RemoveAt(0);
        }

        /**
         * Takes in a note.
         * Prepends it to the melody line (making it the new first note)
         * Returns the new melodyLine.
         */

        public void Prepend(Note firstCounterNote)
        {
            this.AMelodyLine.Insert(0, firstCounterNote);
        }

        public int Length()
        {
            return AMelodyLine.Count;
        }

        public MelodyLine()
        {
            this.AMelodyLine = new List<Note>();
        }
    }
            
        
        
}
