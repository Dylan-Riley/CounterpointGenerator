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
            Console.WriteLine("Input Acceptor(Insert a melodyline): ");

            string InputString = Console.ReadLine();
            List<Note> InputList = new List<Note>();

            foreach(string s in InputString.Split(' '))
            {
                // TODO: Change when input changes drastically
                InputList.Add(new Note(Int32.Parse(s)));
            }

            var Input = new Input(new MelodyLine(InputList));
            Console.WriteLine(Input.ToString());
            return Task.FromResult<IInput>(Input);
        }

    }
}