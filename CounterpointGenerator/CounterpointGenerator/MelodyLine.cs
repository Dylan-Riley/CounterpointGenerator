using System;
using System.Collections.Generic;
using System.Linq;

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
         */
        public void Prepend(Note firstCounterNote)
        {
            this.AMelodyLine.Insert(0, firstCounterNote);
        }

        public int Length()
        {
            // NOT the length as in beats
            // Number of notes in the melody line
            return AMelodyLine.Count;
        }

        public double BeatCount()
        {
            // HAS NOTHING TO DO WITH number of notes
            // Total duration of all notes in melody line
            double runningTotal = 0.0;
            foreach(Note n in AMelodyLine)
            {
                runningTotal += n.Length;
            }
            return runningTotal;
        }

        /*
         * | Note: C5 Length 4 | Note D5 Length 4  |
         * |----|----|----|----|----|----|----|----|  
         * 0    1    2    3    4    5    6    7    8
         * 
         * C5 occupies the entire space 0 up to, but not including, 4
         * GetNoteAtBeatCount(4) should return a reference to the D5 note
         */

        public Note GetNoteAtBeatCount(double beatCount) // 0-indexed
        {
            double max = this.BeatCount();
            if(beatCount > max)
            {
                throw new IndexOutOfRangeException($"Beat Count {beatCount} outside max {max}");
            }

            double runningCount = 0;
            foreach(Note n in AMelodyLine)
            {
                runningCount += n.Length;
                if(runningCount > beatCount)
                {
                    return n;
                }
            }

            // Shouldn't get here
            return null;
        }

        public double GetBeatCountForNoteStart(Note note) // 0-indexed
        {
            double runningCount = 0;
            foreach(Note check in AMelodyLine){
                if (note == check)
                {
                    return runningCount;
                }
                runningCount += check.Length;
            }

            // If not found
            return -1;
        }

        public MelodyLine()
        {
            this.AMelodyLine = new List<Note>();
        }

        public MelodyLine(List<Note> listOfNotes)
        {
            this.AMelodyLine = new List<Note>(listOfNotes);
        }

        public override string ToString()
        {
            return string.Join(", ", AMelodyLine);
        }

    }

}
