using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class MockOutputTranslator: IOutputTranslator
    {
        public MockOutputTranslator()
        {
        }

        public Task TranslateOutput(IOutput output)
        {
            Console.WriteLine("Output is unimplemented. But we got this from generator:");
            Console.WriteLine(output.ToString());
            return Task.CompletedTask;
        }
    }
}
