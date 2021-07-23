using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using CounterpointGenerator;

namespace _3_1_GeneratorTests
{
    [TestClass]
    public class RulesTest
    {
        private readonly List<int> defaultPossibilities = new List<int>()
        {
            -13, -12, -11, -10, -9, -8, -7, -6, -5,
            -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7,
            8, 9, 10, 11, 12, 13
        };
        [TestMethod]
        public void Test_ImperfectConsonance()
        {
            List<int> testRange = new List<int>(defaultPossibilities);
            int spoofCurrentNote = 0; // C5
            RuleInput ri = new RuleInput();
            ri.CurrentNote = spoofCurrentNote;
            ri.Possibilities = testRange;

            ImperfectConsonanceRule icr = new ImperfectConsonanceRule();
            List<int> resultRange = icr.Apply(ri);

            List<int> expectedRange = new List<int>() { -9, -8, -4, -3, 3, 4, 8, 9 };

            CollectionAssert.AreEquivalent(expectedRange, resultRange);
            Console.WriteLine(string.Join(", ", expectedRange));
            Console.WriteLine(string.Join(", ", resultRange));
        }

        [TestMethod]
        public void Test_PerfectConsonance()
        {
            List<int> testRange = new List<int>(defaultPossibilities);
            int spoofCurrentNote = 0; //C5
            RuleInput ri = new RuleInput();
            ri.CurrentNote = spoofCurrentNote;
            ri.Possibilities = testRange;

            PerfectConsonanceRule pcr = new PerfectConsonanceRule();
            List<int> resultRange = pcr.Apply(ri);

            List<int> expectedRange = new List<int>(){ -12, -7, -5, 5, 7, 12 };
            CollectionAssert.AreEquivalent(expectedRange, resultRange);
            Console.WriteLine(string.Join(", ", expectedRange));
            Console.WriteLine(string.Join(", ", resultRange));
        }
    }
}
