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
            -12, -11, -10, -9, -8, -7, -6, -5,
            -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7,
            8, 9, 10, 11, 12
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
            Note spoofCurrentNote = new Note(Constants.C5);
            RuleInput ri = new RuleInput
            {
                CurrentNote = spoofCurrentNote,
                Possibilities = testRange
            };

            ImperfectConsonanceRule icr = new ImperfectConsonanceRule();
            List<Note> resultRangeNote = icr.Apply(ri);

            List<int> expectedRange = new List<int>(Constants.IMPERFECT_INTERVALS);
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
    }
}
