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

        public Note GetNoteAtBeatCount(double beatCount)
        {
            if(beatCount > this.BeatCount())
            {
                throw new IndexOutOfRangeException($"Beat Count {beatCount} outside max {this.BeatCount()}");
            }

            double runningCount = 0;
            foreach(Note n in AMelodyLine)
            {
                runningCount += n.Length;
                if(runningCount >= beatCount)
                {
                    return n;
                }
            }

            // Shouldn't get here
            return null;
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
