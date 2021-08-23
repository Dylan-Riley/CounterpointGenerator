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

        private class RecursiveInput
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
            RecursiveInput ri = new RecursiveInput
            {
                CantusFirmus = inputCantusFirmus,
                EndOn = inputCantusFirmus.Length() - 1,
                TotalNoteLength = inputCantusFirmus.BeatCount(),
                Duration = duration
            };
            return RecursiveGenerateCounterpoint(ri);
        }

        private List<MelodyLine> RecursiveGenerateCounterpoint(RecursiveInput recursiveInput)
        {
            Note currentNote = recursiveInput.CantusFirmus.FirstNote;
            // TODO: UNFINISHED
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
