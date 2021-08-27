using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
            internal double BeatCount { get; set; } = 0;
            internal double TotalBeatCount { get; set; }
            internal double Duration { get; set; }
        }

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusFirmus, double duration)
        {
            RecursiveParameters rp = new RecursiveParameters
            {
                CantusFirmus = inputCantusFirmus,
                EndOn = inputCantusFirmus.Length() - 1,
                TotalBeatCount = inputCantusFirmus.BeatCount(),
                Duration = duration
            };
            return RecursiveGenerateCounterpoint(rp);
        }

        private List<MelodyLine> RecursiveGenerateCounterpoint(RecursiveParameters recurPara)
        {
            Note currentNote = recurPara.CantusFirmus.FirstNote;
            List<Note> possibilitiesBeforeRules = GenerateStartingNotes(currentNote);
            List<Note> possibilitiesAfterRules = UseRules(recurPara, possibilitiesBeforeRules, currentNote);

            List<MelodyLine> solutionList = new List<MelodyLine>();

            List<Note> subListToExplore = _weightSelector.SelectPossibilities(possibilitiesAfterRules, currentNote);

            if(recurPara.BeatCount == recurPara.TotalBeatCount ||
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() - startTime >= recurPara.Duration * 1000)
            {
                return subListToExplore
                    .Select(note => new MelodyLine(new List<Note> { note }))
                    .ToList();
            }

            recurPara.CantusFirmus.RemoveFirstNote();

            foreach(Note explore in subListToExplore)
            {
                // TODO: Rename Count and figure a way to only update it after moving an entire note
                //       through the cantus firmus?
                List<MelodyLine> melodyList = RecursiveGenerateCounterpoint(new RecursiveParameters()
                {
                    CantusFirmus = recurPara.CantusFirmus,
                    PreviousNote = currentNote,
                    PreviousCounterpointNote = explore,
                    Count = recurPara.Count + 1,
                    EndOn = recurPara.EndOn,
                    BeatCount = recurPara.BeatCount + explore.Length,
                    TotalBeatCount = recurPara.TotalBeatCount,
                    Duration = recurPara.Duration
                });

                List<MelodyLine> solution = new List<MelodyLine>();
                foreach (MelodyLine line in melodyList)
                {
                    line.Prepend(explore);
                    solution.Add(line);
                }
                solutionList.AddRange(solution);
            }

            return solutionList;

        }

        private List<Note> UseRules(RecursiveParameters recurPara, List<Note> possibleNotes, Note currentNote)
        {
            // Add more RuleInput below as needed
            // Will likely need fields for current beat count and total beat count
            RuleInput ri = new RuleInput()
            {
                Possibilities = possibleNotes,
                CurrentNote = currentNote,
                Position = recurPara.Count,
                EndOn = recurPara.EndOn,
                PreviousCantus = recurPara.PreviousNote,
                PreviousCounterpoint = recurPara.PreviousCounterpointNote
            };

            ruleApplier = new RuleApplier();
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
