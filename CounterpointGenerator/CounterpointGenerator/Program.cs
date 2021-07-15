using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CounterpointGenerator
{
    class Program
    {
        static Task Main(string[] args)
        {
            // Using dependency injection to easily switch out input/output layers.
            // For more about DI: https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection
            using IHost host = CreateHostBuilder(args).Build();

            return host.RunAsync();
        }


        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<GeneratorHost>()
                            // For now, registering mocks.
                            .AddSingleton<IInputTranslator, MockInputTranslator>()
                            .AddSingleton<IGenerator, MockGenerator>()
                            .AddSingleton<IOutputTranslator, MockOutputTranslator>());
        // Let's make everything a singleton for now.
        // Depending on the implementation might make sense to have generator be transient.

    }
}
