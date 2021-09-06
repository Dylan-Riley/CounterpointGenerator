using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CounterpointGenerator
{
    class GeneratorBeats: IGenerator
    {
        private RuleApplierBeats ruleApplier;
        private IWeightSelect _weightSelector;
        private double startTime;

        public GeneratorBeats(IWeightSelect ws)
        {
            _weightSelector = ws;
        }

        private class GenerateCounterpointImplParameters
        {
            internal MelodyLine CantusFirmus { get; set; }
            internal double BeatCount { get; set; } = 0;
            internal double TotalBeatCount { get; set; }
            internal double Duration { get; set; }
        }

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusFirmus, double duration)
        {
            GenerateCounterpointImplParameters para = new GenerateCounterpointImplParameters
            {
                CantusFirmus = inputCantusFirmus,
                TotalBeatCount = inputCantusFirmus.BeatCount(),
                Duration = duration
            };
            return GenerateCounterpointImpl(para);
        }

        private List<MelodyLine> GenerateCounterpointImpl(GenerateCounterpointImplParameters recurPara)
        {
            if(recurPara.BeatCount == recurPara.TotalBeatCount)
            {
                return null;
            }
            Note currentNote = recurPara.CantusFirmus.GetNoteAtBeatCount(recurPara.BeatCount);
            List<Note> possibilitiesBeforeRules = GenerateStartingNotes(currentNote);
            List<Note> possibilitiesAfterRules = UseRules(recurPara, possibilitiesBeforeRules, currentNote);
            // TODO: Rules might need access to solution thus far somehow, outside of just previous ntoe

            List<MelodyLine> solutionList = new List<MelodyLine>();

            List<Note> subListToExplore = _weightSelector.SelectPossibilities(possibilitiesAfterRules, currentNote);

            if(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - startTime >= recurPara.Duration * 1000)
            {
                return subListToExplore
                    .Select(note => new MelodyLine(new List<Note> { note }))
                    .ToList();
            }

            foreach(Note explore in subListToExplore)
            {
                List<MelodyLine> melodyList = GenerateCounterpointImpl(new GenerateCounterpointImplParameters()
                {
                    CantusFirmus = recurPara.CantusFirmus,
                    BeatCount = recurPara.BeatCount + explore.Length,
                    TotalBeatCount = recurPara.TotalBeatCount,
                    Duration = recurPara.Duration
                });

                if (melodyList == null)
                {
                    // Do nothing
                }
                else
                {
                    List<MelodyLine> solution = new List<MelodyLine>();
                    foreach (MelodyLine line in melodyList)
                    {
                        line.Prepend(explore);
                        solution.Add(line);
                    }
                    solutionList.AddRange(solution);
                }
            }

            return solutionList;

        }

        private List<Note> UseRules(GenerateCounterpointImplParameters recurPara, List<Note> possibleNotes, Note currentNote)
        {
            // Add more RuleInput below as needed
            RuleInput ri = new RuleInput()
            {
                // TODO: Old rules what use previous note *need* fixing!
                Possibilities = possibleNotes,
                CurrentNote = currentNote,
                ExpectedTotalBeatCount = recurPara.TotalBeatCount,
                CurrentBeatCount = recurPara.BeatCount,
                CantusFirmus = recurPara.CantusFirmus
            };

            ruleApplier = new RuleApplierBeats();
            if (ruleApplier.GenerateSet())
            {
                ruleApplier.Applicator(ri);
            }
            else
            {
                throw new Exception("Ruleset did not generate properly!");
            }

            return ri.Possibilities;
        }

        private List<Note> GenerateStartingNotes (Note currentNote)
        {
            List<Note> output = new List<Note>();
            for(int i = 0; i < Constants.GIVE_ME_LOTS_OF_NOTES; i++)
            {
                output.Add(new Note(_weightSelector.GetRandomIntervalFrom(currentNote),
                                    _weightSelector.GetRandomNoteLength()));
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
