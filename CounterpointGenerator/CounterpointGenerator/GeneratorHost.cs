using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CounterpointGenerator
{
    /**
     * This is the main host for the counterpoint generator.
     * For more about the host framework: https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host
     */
    public class GeneratorHost: IHostedService
    {
        private readonly ILogger _logger;
        private readonly IInputTranslator _inputTranslator;
        private readonly IGenerator _generator;
        private readonly IOutputTranslator _outputTranslator;

        public GeneratorHost(
            ILogger<GeneratorHost> logger,
            IInputTranslator inputTranslator,
            IGenerator generator,
            IOutputTranslator outputTranslator)
        {
            _logger = logger;
            _inputTranslator = inputTranslator;
            _generator = generator;
            _outputTranslator = outputTranslator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await RunGenerator();                  
            }
        }

        public async Task RunGenerator()
        {
            // Consider adding graceful stopping on cancellation request.
            var input = await _inputTranslator.GetInput();
            var output = await _generator.Generate(input);
            await _outputTranslator.TranslateOutput(output);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
