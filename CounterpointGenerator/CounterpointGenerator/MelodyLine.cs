using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterpointGenerator
{
    public class MelodyLine
    {
        public List<Note> AMelodyLine { get; set; }
        public Dictionary<string, int> TimeSignature { get; }


        public Note FirstNote {
            get
            {
                return this.AMelodyLine[0];
            }
        }

        public Note LastNote
        {
            get
            {
                // This is an "index operator" used to count from the end
                return this.AMelodyLine[^1];
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

        public void Add(Note n)
        {
            this.AMelodyLine.Add(n);
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
            TimeSignature.Add(Constants.TIMESIG_BEATS, Constants.DEFAULT_MEASURE_BEATS);
            TimeSignature.Add(Constants.TIMESIG_VALUE, Constants.DEFAULT_BEAT_VALUE);
        }

        public MelodyLine(List<Note> listOfNotes)
        {
            this.AMelodyLine = new List<Note>(listOfNotes);
            TimeSignature.Add(Constants.TIMESIG_BEATS, Constants.DEFAULT_MEASURE_BEATS);
            TimeSignature.Add(Constants.TIMESIG_VALUE, Constants.DEFAULT_BEAT_VALUE);
        }

        public MelodyLine(List<Note> listOfNotes, Dictionary<string, int> timeSig)
        {
            this.AMelodyLine = new List<Note>(listOfNotes);
            TimeSignature = timeSig;
        }

        public MelodyLine(MelodyLine otherLine, Note addNote)
        {
            // Helper constructor to avoid mutation in GeneratorBeats
            this.AMelodyLine = new List<Note>(otherLine.AMelodyLine);
            this.TimeSignature = otherLine.TimeSignature;

            this.AMelodyLine.Add(addNote);
        }

        public override string ToString()
        {
            return string.Join(", ", AMelodyLine) + $" Beat count:{this.BeatCount()}";
        }

    }

}
