using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * instantiated of OutputTranslator.
     */
    public class OutputTranslator : IOutputTranslator
    {
        public OutputTranslator()
        {
        }

        public Task TranslateOutput(IOutput output)
        {
            Console.WriteLine("Output from counterpoint generator:");
            Console.WriteLine(output.ToString());
            return Task.CompletedTask;
        }

    }
}
