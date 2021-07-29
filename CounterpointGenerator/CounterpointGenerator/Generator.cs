﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class Generator: IGenerator
    {
        private RuleApplier ruleApplier;

        private List<MelodyLine> GenerateCounterpoint(MelodyLine inputCantusfirmus)
        {
            return GenerateCounterpointForNoteStack(inputCantusfirmus, null, null, 0);
        }

        private List<MelodyLine> GenerateCounterpointForNoteStack(MelodyLine m, Note? previousNote, Note? previousCounterNote, int count)
        {
            Note n = m.FirstNote;
            List<Note> possibilitiesAfterRules = CounterpointForNote(n, previousNote, previousCounterNote, m.Length(), count);

            List<MelodyLine> solutionList = new List<MelodyLine>();
            IWeightSelect weightSelector = new WeightSelect();
            List<Note> subListToExplore = weightSelector.SelectPossibilities(possibilitiesAfterRules);

            m.RemoveFirstNote();
            foreach (Note p in subListToExplore)
            {
         
                List<MelodyLine> melodyList = GenerateCounterpointForNoteStack(m, n, p, count + 1);
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

        private List<Note> CounterpointForNote(Note n, Note prevN, Note preCtp, int melodyLength, int generateCount)
        {
            List<Note> possibleNotes = GenerateNoteAtRegion(n);
            // Possibilities, currentNote, position, length, previousCantus, previousCounterpoint
            RuleInput ruleInput = new RuleInput(possibleNotes, n, generateCount, melodyLength, prevN, preCtp);

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
            int upperPitch = n.Pitch + 13; // +1 octave
            int lowerPitch = n.Pitch - 13; // -1 octave
            List<Note> output = new List<Note>();
            for (int addPitch = lowerPitch; addPitch <= upperPitch; addPitch++)
            {
                output.Add(new Note(addPitch));
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