using Jalasoft.TeamUp.Resumes.API;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Jalasoft.TeamUp.Resumes.API
{
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IHealthService, HealthService>();
            builder.Services.AddScoped<IHealthRepository, HealthRepository>();
            builder.Services.AddScoped<IResumesService, ResumesService>();
            builder.Services.AddScoped<IRepository<Resume>, ResumesRepository>();
            builder.Services.AddScoped<IRepository<Skill>, SkillsInMemoryRepository>();
        }
    }
}
