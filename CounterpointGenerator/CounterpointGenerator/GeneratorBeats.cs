using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    class GeneratorBeats: IGenerator
    {
        private RuleApplier ruleApplier;
        private IWeightSelect _weightSelector;
        private double startTime;

        public GeneratorBeats(IWeightSelect ws)
        {
            _weightSelector = ws;
        }

        private class RecursiveParameters
        {
            internal MelodyLine CantusFirmus { get; set; }
            internal Note PreviousNote { get; set; } = null;
            internal Note PreviousCounterpointNote { get; set; } = null;
            internal int Count { get; set; } = 0;
            internal int EndOn { get; set; }
            internal double TotalNoteLength { get; set; }
            internal double Duration { get; set; }
        }

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusFirmus, double duration)
        {
            RecursiveParameters rp = new RecursiveParameters
            {
                CantusFirmus = inputCantusFirmus,
                EndOn = inputCantusFirmus.Length() - 1,
                TotalNoteLength = inputCantusFirmus.BeatCount(),
                Duration = duration
            };
            return RecursiveGenerateCounterpoint(rp);
        }

        private List<MelodyLine> RecursiveGenerateCounterpoint(RecursiveParameters recurPara)
        {
            Note currentNote = recurPara.CantusFirmus.FirstNote;
            double newNoteDuration = _weightSelector.GetRandomNoteLength();
            
        }

        private List<int> UseRules(RecursiveParameters recurPara, Note currentNote, double newNoteDuration)
        {
            
        }

        private List<Note> GenerateIntervalRegion (Note currentNote, double newNoteDuration)
        {
            List<int> intervals = new List<int>(Constants.PERFECT_INTERVALS);
            intervals.AddRange(Constants.IMPERFECT_INTERVALS);

            List<Note> output = new List<Note>();

            foreach(int interval in intervals)
            {
                output.Add(new Note(currentNote.Pitch + interval, newNoteDuration));
            }

            return output;
        }

        public Task<IOutput> Generate(IInput input)
        {
            startTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            Output output = new Output();
            output.Cantus = GenerateCounterpoint(input.Cantus, input.userPreference);
            return Task.FromResult<IOutput>(output);
        }
    }
}
