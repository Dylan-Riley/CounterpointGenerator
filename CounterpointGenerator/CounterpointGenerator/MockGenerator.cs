using System;
using System.Threading.Tasks;

namespace CounterpointGenerator
{
    /**
     * A mocked implementation of the counterpoint generator.
     */
    public class MockGenerator: IGenerator
    {
        public MockGenerator()
        {
        }

        public Task<IOutput> Generate(IInput input)
        {
            Console.WriteLine("generate: not implemented yet.\n" +
                "Received input:");
            Console.WriteLine(input.ToString());

            Console.WriteLine("Let's just pass along the input for now.");
            var mockOutput = new Output();
            mockOutput.Cantus = input.Cantus;
            return Task.FromResult<IOutput>(mockOutput);
        }
    }
}
