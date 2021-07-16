using System;

namespace GenerateCounterpoint
{
    public class Generator
    {
        public List<MelodyLines> GenerateCounterpoint(MelodyLines inputCantusfirmus)
        {
            return GenerateCounterpointForNoteStack();
        }

        public List<MelodyLines> GenerateCounterpointForNoteStack(MelodyLines m, Note previousNote, Note previousCounterNote)  
        {
            Note n = m.firstNote;
            Set<Note> possibilitiesAfterRules = CounterpointForNote(n, previousNote, previousCounterNote);
            
            List<MelodyLines> solutionList = new List<MelodyLines>();
            WeightSelect weightSelector = new WeightSelect();
            Set<Note> subsetToExplore = weightSelector.selectPosibilities(possibilitiesAfterRules);

            foreach(Note p in subsetToExplore)
            {
                MelodyLines newMelodyLine = m.removeFirstNote();
                List<MelodyLines> melodySet = GenerateCounterpointForNoteStack(newMelodyLine, n, p);
                List<MelodyLines> solution = new List<MelodyLines>();
                foreach(var line in melodySet)
                {
                    var newLine = line.prepend(p);
                    solution.add(newLine);
                }
                solutionList.append(solution);
            }

        }

        public Set<Note> GenerateCounterpointForNote(Note n, Note prevN, Note preCtp)
        {
            
        }
    }
}