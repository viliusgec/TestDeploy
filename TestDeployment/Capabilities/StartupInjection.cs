using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDeployment.Capabilities
{
    public static class StartupInjection
    {
        public static IServiceCollection ConfigureInjection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Inject localization
            services.AddControllers().AddDataAnnotationsLocalization();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Inject database
            services.AddDatabase(configuration);

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("Database")["ConnectionString"];
            var databasePassword = configuration.GetSection("Database")["Password"];
            connectionString = connectionString.Replace("replaceWithPass", databasePassword);
/*            services.AddScoped<ICurrencyRatesRepository>(provider =>
            {
                var mapper = provider.GetRequiredService<IMappingService>();
                return new CurrencyRatesRepository(connectionString, mapper);
            });
            services.AddScoped<IProviderRepository>(_ => new ProviderRepository(connectionString));
            services.AddScoped<IOperationRepository>(provider =>
            {
                var mapper = provider.GetRequiredService<IMappingService>();
                return new OperationRepository(connectionString, mapper);
            });*/
        }
    }


}
