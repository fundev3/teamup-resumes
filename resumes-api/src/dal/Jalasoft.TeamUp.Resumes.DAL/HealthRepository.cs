namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class HealthRepository : IHealthRepository
    {
        public Health GetHealth()
        {
            var healths = new Health { Id = Guid.NewGuid(), Message = "I'm resumes-api and I'm alive and running! ;)" };
            return healths;
        }

        public Health PostHealth()
        {
            var healths = new Health { Id = Guid.NewGuid(), Message = "I'm create resumes api and I'm alive and running! ;)" };
            return healths;
        }
    }
}
