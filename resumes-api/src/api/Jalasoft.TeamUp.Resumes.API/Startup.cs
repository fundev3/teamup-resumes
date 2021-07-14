using Jalasoft.TeamUp.Resumes.API;
using Jalasoft.TeamUp.Resumes.Core;
using Jalasoft.TeamUp.Resumes.Core.Interfaces;
using Jalasoft.TeamUp.Resumes.DAL;
using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Jalasoft.TeamUp.Resumes.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IHealthService, HealthService>();
            builder.Services.AddTransient<IHealthRepository, HealthRepository>();
        }
    }
}
