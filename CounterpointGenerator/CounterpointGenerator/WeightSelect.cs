using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterpointGenerator
{
    public class WeightSelect: IWeightSelect
    {
        private Random _rnd = new Random();
        
        // Maximum number of notes to select into
        public int maximumCount { get; set; } = 5;
        // Maximum number of times the select if statements can fail to find anything before hard picking something
        public int maximumFail { get; set; } = 5;
        // Chance that Imperfect Consonance is selected
        public int imperfectChance { get; set; } = 75;

        public List<Note> SelectPossibilities(List<Note> selectFrom, Note currentNote)
        {
            if (selectFrom.Count <= maximumCount)
            {
                // Don't bother with weight selecting if there's already less than five notes to pick from
                return selectFrom;
            }

            List<Note> output = new List<Note>();
            List<Note> mutate = new List<Note>(selectFrom);
            Shuffle(mutate);
            int failCount = 0;

            while(output.Count <= maximumCount)
            {
                int chance = _rnd.Next(1, 101);

                if(chance <= imperfectChance)
                {
                    Note n = mutate.Find(note => Constants.IMPERFECT_INTERVALS.Contains(note.Pitch - currentNote.Pitch));
                    if(n == null)
                    {
                        failCount++;
                    }
                    else
                    {
                        output.Add(n);
                        mutate.Remove(n);
                        failCount = 0;
                    }
                }
                else
                {
                    Note n = mutate.Find(note => Constants.PERFECT_INTERVALS.Contains(note.Pitch - currentNote.Pitch));
                    if(n == null)
                    {
                        failCount++;
                    }
                    else
                    {
                        output.Add(n);
                        mutate.Remove(n);
                        failCount = 0;
                    }
                }

                if (failCount > maximumFail)
                {
                    // Only find our way into here if failCount climbs on successive fails
                    int justPickOne = _rnd.Next(0, mutate.Count);
                    output.Add(mutate.ElementAt(justPickOne));
                    mutate.RemoveAt(justPickOne);
                    failCount = 0; // Reset to 0
                }
            }

            return output;
        }

        private void Shuffle<T>(IList<T> list)
        {
            // Shuffle method based on Fisher-Yates shuffle
            // Shamelessly stolen from https://stackoverflow.com/questions/273313/randomize-a-listt
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Adjust these to adjust note length weighting
        public int wholeNoteWeight { get; set; } = 10;
        public int halfNoteWeight { get; set; } = 30;
        public int quarterNoteWeight { get; set; } = 20;
        public int eightthNoteWeight { get; set; } = 5;
        public int sixteenthNoteWeight { get; set; } = 2;

        public double GetRandomNoteLength()
        {
            List<WeightedItem> lengths = new List<WeightedItem>()
            {
                new WeightedItem(Constants.WHOLE_NOTE_LENGTH, wholeNoteWeight),
                new WeightedItem(Constants.HALF_NOTE_LENGTH, halfNoteWeight),
                new WeightedItem(Constants.QUARTER_NOTE_LENGTH, quarterNoteWeight),
                new WeightedItem(Constants.EIGHTH_NOTE_LENGTH, eightthNoteWeight),
                new WeightedItem(Constants.SIXTEENTH_NOTE_LENGTH, sixteenthNoteWeight)
            };

            int totalWeight = 0;
            foreach(WeightedItem i in lengths)
            {
                totalWeight += i.Weight;
            }

            int rnd = _rnd.Next(0, totalWeight);
            double output = 0;

            foreach(WeightedItem i in lengths)
            {
                if(rnd < i.Weight)
                {
                    output = i.Value;
                    break;
                }
                rnd -= i.Weight;
            }

            return output;
        }

        // Do not set below 12!
        public int weightModifier { get; set; } = 15;

        public int GetRandomIntervalFrom(Note n)
        {
            if(weightModifier < 12)
            {
                throw new Exception("Setting weight modifier below 12 will break things");
            }

            List<WeightedItem> weightedIntervals = new List<WeightedItem>();
            // Build a weighted list of intervals where the larger the interval the smaller the weight
            foreach(int interval in Constants.PERFECT_INTERVALS.Concat(Constants.IMPERFECT_INTERVALS))
            {
                weightedIntervals.Add(new WeightedItem(interval + n.Pitch, weightModifier - Math.Abs(interval)));
            }

            int totalWeight = 0;
            foreach(WeightedItem wi in weightedIntervals)
            {
                totalWeight += wi.Weight;
            }

            int rnd = _rnd.Next(0, totalWeight);
            int output = -1000;

            foreach(WeightedItem wi in weightedIntervals)
            {
                if(rnd < wi.Weight)
                {
                    output = (int) wi.Value;
                    break;
                }
                rnd -= wi.Weight;
            }

            return output;
        }

        private struct WeightedItem
        {
            public double Value { get; }
            public int Weight { get; }

            public WeightedItem(double value, int weight)
            {
                this.Value = value;
                this.Weight = weight;
            }
        }
    }
}
