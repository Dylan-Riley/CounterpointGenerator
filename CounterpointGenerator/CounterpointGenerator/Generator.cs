using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class Generator: IGenerator
    {
        IRuleApplier _applier;

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusfirmus)
        {
            return GenerateCounterpointForNoteStack(inputCantusfirmus, null, null);
        }

        private List<MelodyLine> GenerateCounterpointForNoteStack(MelodyLine m, Note? previousNote, Note? previousCounterNote)
        {
            Note n = m.FirstNote;
            List<Note> possibilitiesAfterRules = CounterpointForNote(n, previousNote, previousCounterNote);

            List<MelodyLine> solutionList = new List<MelodyLine>();
            IWeightSelect weightSelector = new WeightSelect();
            List<Note> subListToExplore = weightSelector.SelectPossibilities(possibilitiesAfterRules);

            MelodyLine newMelodyLine = m.RemoveFirstNote();
            foreach (Note p in subListToExplore)
            {
         
                List<MelodyLine> melodyList = GenerateCounterpointForNoteStack(newMelodyLine, n, p);
                List<MelodyLine> solution = new List<MelodyLine>();
                foreach (MelodyLine line in melodyList)
                {
                    MelodyLine newLine = line.Prepend(p);
                    solution.Add(newLine);
                }
                solutionList.AddRange(solution);
            }

            return solutionList;

        }

        private List<Note> CounterpointForNote(Note n, Note prevN, Note preCtp)
        {
            List<Note> possibleNotes = GenerateNoteAtRegion(n);
            List<Note> possibleNotesAfterRules = new List<Note>();
            foreach(IRule r in Dictionary[input.userPreference])
            {
                List<Note> newPossibleNotes = r.Apply(possibleNotes);
                possibleNotesAfterRules.AddRange(newPossibleNotes);
            }
            return possibleNotesAfterRules;
        }

        private List<Note> GenerateNoteAtRegion(Note n)
        {
            throw new NotImplementedException();
        }

        public Task<IOutput> Generate(IInput input)
        {
            Output generateOutput = new Output();
            _applier.setUserPreferences(input.RulePreferences);
            generateOutput.Cantus = GenerateCounterpoint(input.Cantus);
            return Task.FromResult<IOutput>(generateOutput);
        }
    }
}