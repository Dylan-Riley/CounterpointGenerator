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
    }
}
