using System;
using System.Collections.Generic;

namespace CounterpointGenerator
{
    /**
     * A mocked implementation of the output translator layer.
     */
    public class Output: IOutput
    {
        public Output()
        {
        }

        public Output(double beatCount)
        {
            this.BeatCount = beatCount;
        }

        public List<MelodyLine> Cantus { get; set; }
        public List<MelodyLine> Complete;
        public List<MelodyLine> CloseTo;
        private double BeatCount;

        public override string ToString()
        {
            return Cantus.Count + "\n" + string.Join("\n", Cantus);
        }

        public void Collect()
        {
            Complete = new List<MelodyLine>();
            CloseTo = new List<MelodyLine>();
            foreach(MelodyLine ml in Cantus)
            {
                if (ml.BeatCount() == this.BeatCount)
                {
                    Complete.Add(ml);
                }
                else if (ml.BeatCount() / this.BeatCount >= (1 - (Constants.OUTPUT_ERROR_MARGIN / 100.0)))
                {
                    CloseTo.Add(ml);
                }
            }
        }

        public string CompleteToString()
        {
            //return Complete.Count + " Completed melodylines\n" + string.Join("\n", Complete);
            return $"{Complete.Count} Completed melodylines\n{string.Join("\n", Complete)}";
        }

        public string CloseToToString()
        {
            return $"{CloseTo.Count} Melodylines within {Constants.OUTPUT_ERROR_MARGIN}% of completion\n" +
                   $"{string.Join("\n", CloseTo)}";
        }
    }
}
