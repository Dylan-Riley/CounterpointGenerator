using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class Generator: IGenerator
    {
        //IRuleApplier _applier;

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

            foreach (Note p in subListToExplore)
            {
                MelodyLine newMelodyLine = m.RemoveFirstNote();
                List<MelodyLine> melodyList = GenerateCounterpointForNoteStack(newMelodyLine, n, p);
                List<MelodyLine> solution = new List<MelodyLine>();
                foreach (var line in melodyList)
                {
                    var newLine = line.Prepend(p);
                    solution.Add(newLine);
                }
                solutionList.AddRange(solution);
            }

            return solutionList;

        }

        private List<Note> CounterpointForNote(Note n, Note prevN, Note preCtp)
        {
            throw new NotImplementedException();
        }

        public Task<IOutput> Generate(IInput input)
        {
            // GenerateCounterpoint(input.Cantus);
            // _applier.setUserPreferences(input.RulePreferences);
            throw new NotImplementedException();
        }
    }
}