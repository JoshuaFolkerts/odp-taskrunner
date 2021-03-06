using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ODP.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ODPContentRunner
{
    public class Program
    {
        public static IConfigurationRoot configuration;

        public static async Task Main(string[] args)
        {
            Console.Clear();
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
            var odpTaskRunner = serviceProvider.GetService<ODPTaskRunner>();
            await odpTaskRunner.Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddOptions();

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.Configure<AppSettings>(configuration.GetSection("appSettings"));

            services
                .AddSingleton<IODPService, ODPService>()
                .AddSingleton<IContentGeneratorService, ContentGeneratorService>()
                .AddSingleton<IRandomGenerator, RandomGenerator>()
                .AddSingleton<IJourneyGenerator, JourneyGenerator>()
                .AddTransient<ODPTaskRunner>();

            return services;
        }
    }
}