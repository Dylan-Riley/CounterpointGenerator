using System;
using System.Collections.Generic;
using System.Linq;

namespace CounterpointGenerator
{
    public class WeightSelect: IWeightSelect
    {
        private Random _rnd = new Random();
        public int maximumCount { get; set; } = 5;
        public int maximumFail { get; set; } = 5;

        public List<Note> SelectPossibilities(List<Note> selectFrom, Note currentNote)
        {
            if (selectFrom.Count <= maximumCount)
            {
                // Don't bother with weight selecting if there's already less than five notes to pick from
                return selectFrom;
            }

            List<Note> output = new List<Note>();
            List<Note> mutate = new List<Note>(selectFrom);
            int failCount = 0;

            while(output.Count <= maximumCount)
            {
                int chance = _rnd.Next(1, 101);

                if(chance <= 75)
                {
                    failCount++; // Preemptively increment this
                    foreach(Note n in mutate)
                    {
                        if (Constants.IMPERFECT_INTERVALS.Contains(n.Pitch - currentNote.Pitch))
                        {
                            output.Add(n);
                            mutate.Remove(n);
                            failCount = 0; // Reset to 0 on successful
                            break;
                        }
                    }
                }
                else
                {
                    failCount++; // Preemptively increment this
                    foreach(Note n in mutate)
                    {
                        if (Constants.PERFECT_INTERVALS.Contains(n.Pitch - currentNote.Pitch))
                        {
                            output.Add(n);
                            mutate.Remove(n);
                            failCount = 0; // Reset to 0 on successful
                            break;
                        }
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
    }
}
