using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    public class OutputTranslator : IOutputTranslator
    {
        public OutputTranslator()
        {
        }

        public Task TranslateOutput(IOutput output)
        {
            output.Collect();
            
            Console.WriteLine("Output from counterpoint generator:");
            Console.WriteLine(output.CompleteToString() + "\n");
            Console.WriteLine(output.CloseToToString());
            
            return Task.CompletedTask;
        }

    }
}
