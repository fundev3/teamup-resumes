namespace Jalasoft.TeamUp.Resumes.Core
{
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class HealthService : IHealthService
    {
        private readonly IHealthRepository healthRepository;

        public HealthService(IHealthRepository healthRepository)
        {
            this.healthRepository = healthRepository;
        }

        public Health GetHealth()
        {
            return this.healthRepository.GetHealth();
        }
    }
}
