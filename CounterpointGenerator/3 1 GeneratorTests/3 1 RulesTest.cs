using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CounterpointGenerator;
using System.Linq;

namespace _3_1_GeneratorTests
{
    [TestClass]
    public class RulesTest
    {
        private readonly List<int> intervalIntList = new List<int>()
        {
            -13, -12, -11, -10, -9, -8, -7, -6, -5,
            -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7,
            8, 9, 10, 11, 12, 13
        };
        private List<Note> defaultNoteList = new List<Note>();

        // Runs once before each test
        [TestInitialize]
        public void NoteListInit()
        {
            foreach (int pitch in intervalIntList)
            {
                defaultNoteList.Add(new Note(pitch));
            }
        }

        // Runs once after each test
        [TestCleanup]
        public void TearDown()
        {
            defaultNoteList = new List<Note>();
        }

        [TestMethod]
        public void Test_ImperfectConsonance()
        {
            List<Note> testRange = new List<Note>(defaultNoteList);
            Note spoofCurrentNote = new Note(0); // C5
            RuleInput ri = new RuleInput
            {
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange
            };

            ImperfectConsonanceRule icr = new ImperfectConsonanceRule();
            List<Note> resultRangeNote = icr.Apply(ri);

            List<int> expectedRange = new List<int>() { -9, -8, -4, -3, 3, 4, 8, 9 };
            List<int> resultRange = new List<int>(resultRangeNote.Select(n => n.Pitch));

            Console.WriteLine(string.Join(", ", expectedRange));
            Console.WriteLine(string.Join(", ", resultRange));
            CollectionAssert.AreEquivalent(expectedRange, resultRange);
        }

        [TestMethod]
        public void Test_PerfectConsonance()
        {
            List<Note> testRange = new List<Note>(defaultNoteList);
            Note spoofCurrentNote = new Note(0); //C5
            RuleInput ri = new RuleInput
            {
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange
            };

            PerfectConsonanceRule pcr = new PerfectConsonanceRule();
            List<Note> resultRangeNote = pcr.Apply(ri);

            List<int> expectedRange = new List<int>(){ -12, -7, -5, 5, 7, 12 };
            List<int> resultRange = new List<int>(resultRangeNote.Select(n => n.Pitch));

            Console.WriteLine(string.Join(", ", expectedRange));
            Console.WriteLine(string.Join(", ", resultRange));
            CollectionAssert.AreEquivalent(expectedRange, resultRange);
        }

        [TestMethod]
        public void Test_Battuta()
        {
            List<Note> testRange = new List<Note>(defaultNoteList);
            Note spoofCurrentNote = new Note(0); //C5
            BattutaRule br = new BattutaRule();

            RuleInput wrongPosition = new RuleInput
            {
                // Should return just Possibilities since Position == 0
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange,
                Position = 0
            };
            List<Note> wrongPositionResultNote = br.Apply(wrongPosition);

            // Did nothing happen?
            CollectionAssert.AreEquivalent(testRange, wrongPositionResultNote);

            RuleInput cantusUp = new RuleInput
            {
                // Cantus is stepping up, counterpoint is the higher voice
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange,
                Position = 1,
                PreviousCantus = new Note(-1), // B5
                PreviousCounterpoint = new Note(7) // G5
            };
            List<Note> cantUpResultNote = br.Apply(cantusUp);

            List<int> expectedRange = new List<int>(intervalIntList);
            expectedRange.Remove(13);
            List<int> resultRange = new List<int>(cantUpResultNote.Select(n => n.Pitch));
            // Was 8va removed as a possibility?
            CollectionAssert.AreEquivalent(expectedRange, resultRange);

            // Cantus is stepping up, counterpoint is lower voice
            // Should just return a new list equal to Possibilities
            cantusUp.PreviousCounterpoint = new Note(-5); // G4
            List<Note> cantUpResultNote2 = br.Apply(cantusUp);

            // Did nothing happen?
            CollectionAssert.AreEquivalent(cantusUp.Possibilities, cantUpResultNote2);

            RuleInput cantusDown = new RuleInput
            {
                // Cantus is stepping down, counterpoint is lower voice
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange,
                Position = 1,
                PreviousCantus = new Note(5), // F5
                PreviousCounterpoint = new Note(-1) // B5
            };
            List<Note> cantDwnResultNote = br.Apply(cantusDown);

            expectedRange = new List<int>(intervalIntList);
            expectedRange.Remove(-13);
            resultRange = new List<int>(cantDwnResultNote.Select(n => n.Pitch));
            // Was 8vb removed as a possibility?
            CollectionAssert.AreEquivalent(expectedRange, resultRange);

            // Cantus is stepping down, counterpoint is higher voice
            // Should just return a new list equal to Possibilities
            cantusDown.PreviousCounterpoint = new Note(13); // C6
            List<Note> cantDwnResultNote2 = br.Apply(cantusDown);

            // Did nothing happen?
            CollectionAssert.AreEquivalent(cantusDown.Possibilities, cantDwnResultNote2);
        }

        [TestMethod]
        public void Test_Penultimate()
        {

            //TODO: This needs to be redone because some stuff changed in the meeting


            List<Note> testRange = new List<Note>(defaultNoteList);
            Note spoofCurrentNote = new Note(0); //C5

            // First, out of position test
            RuleInput ri = new RuleInput
            {
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange,
                Position = 0,
                Length = 10
            };

            PenultimateNoteRule pnr = new PenultimateNoteRule();
            List<Note> outOfPositionResult = pnr.Apply(ri);
            // Did nothing happen?
            CollectionAssert.AreEquivalent(testRange, outOfPositionResult);

            // Set the position to be in the correct space
            ri.Position = 9;
            List<Note> resultRangeNote = pnr.Apply(ri);

            List<int> expectedRange = new List<int>() { -3, 3 };
            List<int> resultRange = new List<int>(resultRangeNote.Select(n => n.Pitch));
            CollectionAssert.AreEquivalent(expectedRange, resultRange);

        }
    }
}
