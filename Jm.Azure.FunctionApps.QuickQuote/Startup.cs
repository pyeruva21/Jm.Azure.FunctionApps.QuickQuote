using AutoMapper;
using Jm.Azure.FunctionApps.QuickQuote.Core;
using Jm.Azure.FunctionApps.QuickQuote.Functions;
using Jm.Azure.FunctionApps.QuickQuote.Integration.Repository;
using Jm.Azure.FunctionApps.QuickQuote.Intergration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;

[assembly: FunctionsStartup(typeof(Jm.Azure.FunctionApps.Policies.Functions.Startup))]
namespace Jm.Azure.FunctionApps.Policies.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddTransient<ISmartCommRepository, SmartCommRepository>();
            builder.Services.AddTransient<ISmartCommConfiguration, SmartCommFileConfiguration>();
            builder.Services.AddTransient<IAppSettings, AppSettings>();
            builder.Services.AddTransient<IKeyVaultSettings, KeyVaultSettings>();
            builder.Services.AddTransient<IRestClient, RestClient>();
            builder.Services.AddTransient<IPolicyRepository, PolicyRepository>();
        }
    }
}
