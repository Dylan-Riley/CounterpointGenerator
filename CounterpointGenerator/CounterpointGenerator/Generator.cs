using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class Generator: IGenerator
    {
        private RuleApplier ruleApplier;

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusfirmus)
        {
            return GenerateCounterpointForNoteStack(inputCantusfirmus, null, null, 0, inputCantusfirmus.Length()-1);
        }

        private List<MelodyLine> GenerateCounterpointForNoteStack(MelodyLine m, Note? previousNote, Note? previousCounterNote, int count, int endOn)
        {
            Note n = m.FirstNote;
            List<Note> possibilitiesAfterRules = CounterpointForNote(n, previousNote, previousCounterNote, endOn, count);

            List<MelodyLine> solutionList = new List<MelodyLine>();

            // Rules need a RuleInput object input to function
            RuleInput ri = new RuleInput
            {
                Possibilities = possibilitiesAfterRules,
                CurrentNote = n,
                Position = count,
                EndOn = endOn,
                PreviousCantus = previousNote,
                PreviousCounterpoint = previousCounterNote
            };
            IWeightSelect weightSelector = new WeightSelect(ri);
            List<Note> subListToExplore = weightSelector.SelectPossibilities();

            if (endOn == count)
            {
                return subListToExplore
                    .Select(note => new MelodyLine(new List<Note> { note }))
                    .ToList();
            }

            m.RemoveFirstNote();
            
            foreach (Note p in subListToExplore)
            {

                List<MelodyLine> melodyList = GenerateCounterpointForNoteStack(new MelodyLine(m.AMelodyLine), n, p, count + 1, endOn);
                List<MelodyLine> solution = new List<MelodyLine>();
                foreach (MelodyLine line in melodyList)
                {
                    line.Prepend(p);
                    solution.Add(line);
                }
                solutionList.AddRange(solution);
            }

            return solutionList;

        }

        private List<Note> CounterpointForNote(Note n, Note prevN, Note preCtp, int endOn, int generateCount)
        {
            List<Note> possibleNotes = GenerateNoteAtRegion(n);

            RuleInput ruleInput = new RuleInput
            {
                Possibilities = possibleNotes,
                CurrentNote = n,
                Position = generateCount,
                EndOn = endOn,
                PreviousCantus = prevN,
                PreviousCounterpoint = preCtp
            };

            ruleApplier = new RuleApplier();
            if (ruleApplier.GenerateSet())
            {
                ruleApplier.Applicator(ruleInput);
            }
            else
            {
                throw new Exception("Ruleset did not generate properly!");
            }

            return ruleInput.Possibilities;
        }

        private List<Note> GenerateNoteAtRegion(Note n)
        {
            // Only make notes from the correct intervals in the first place
            List<int> intervals = new List<int>(Constants.PERFECT_INTERVALS);
            intervals.AddRange(Constants.IMPERFECT_INTERVALS);

            List<Note> output = new List<Note>();

            foreach (int interval in intervals)
            {
                // TODO: Fix for note length when that becomes something to worry about
                output.Add(new Note(n.Pitch + interval));
            }
            return output;
        }

        public Task<IOutput> Generate(IInput input)
        {
            Output generateOutput = new Output();
            //_applier.setUserPreferences(input.RulePreferences);
            generateOutput.Cantus = GenerateCounterpoint(input.Cantus);
            return Task.FromResult<IOutput>(generateOutput);
        }
    }
}