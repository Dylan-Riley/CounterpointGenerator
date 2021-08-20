using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * implementation of the layer that translates user input to intermediate representation.
     */
    public class InputTranslator : IInputTranslator
    {
        public InputTranslator()
        {
        }

        public Task<IInput> GetInput()
        {
            // Melody Line input
            Console.WriteLine("Input Acceptor: ");
            Console.WriteLine("Insert a MelodyLine: ");
            // Input expected to be something like "0 16, 0 16, 0 16"
            // (int space double comma space)* (int space double)
            string InputString = Console.ReadLine();
            
            List<Note> InputList = new List<Note>();
            foreach(string s in InputString.Split(' '))
            {
                // TODO: Change when input changes drastically
                InputList.Add(new Note(int.Parse(s)));
            }

            // Generation time input
            Console.WriteLine("Insert a prefered generating duration(in seconds): ");
            string inputPreference = Console.ReadLine();
            double userPreference = 0;
            bool checkTime = true;
            while (checkTime)
            {
                if (inputPreference == "")
                {
                    // Catch this first as it does weird things with TryParse
                    userPreference = Constants.DEFAULT_TIME_LIMIT;
                    checkTime = false;
                }
                else if (Double.TryParse(inputPreference, out userPreference))
                {
                    // TryParse sets the out parameter on check
                    // Only returns true if a double was successfully parsed
                    checkTime = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid double for time in seconds! Try again!");
                }
            }

            var input = new Input(new MelodyLine(InputList), userPreference);
            Console.WriteLine(input.ToString());
            return Task.FromResult<IInput>(input);
        }

    }
}