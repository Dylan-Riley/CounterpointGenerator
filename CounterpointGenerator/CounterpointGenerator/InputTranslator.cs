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
            Console.WriteLine("Input Acceptor: ");
            Console.WriteLine("Insert a MelodyLine: ");
            string InputString = Console.ReadLine();

            Console.WriteLine("Insert a prefered generating duration(in seconds): ");
            string inputPreference = Console.ReadLine();
            double userPreference;
            if (inputPreference == "")
            {
                userPreference = Input.DEFAULT_PREFERENCE;
            }
            else
            {
                userPreference = double.Parse(inputPreference);
            }
            
            List<Note> InputList = new List<Note>();
            foreach(string s in InputString.Split(' '))
            {
                // TODO: Change when input changes drastically
                InputList.Add(new Note(int.Parse(s)));
            }

            var input = new Input(new MelodyLine(InputList), userPreference);
            Console.WriteLine(input.ToString());
            return Task.FromResult<IInput>(input);
        }

    }
}