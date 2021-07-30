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
            MelodyLine InputString = new MelodyLine(Console.ReadLine());
            var Input = new Input(InputString);
            Console.WriteLine(Input.ToString());
            return Task.FromResult<IInput>(Input);
        }
    }
}