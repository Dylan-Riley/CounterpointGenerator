using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class MockInputTranslator: IInputTranslator
    {
        public MockInputTranslator()
        {
        }

        public Task<IInput> GetInput()
        {
            Console.WriteLine("Input: unimplemented. Just press any key.");
            Console.ReadKey();
            var mockInput = new Input(new List<int> { 1, 2, 3 });
            Console.WriteLine("Let's pretend this was the input:");
            Console.WriteLine(mockInput.ToString());
            return Task.FromResult<IInput>(mockInput);
        }
    }
}
