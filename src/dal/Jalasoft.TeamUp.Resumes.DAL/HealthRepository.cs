namespace Jalasoft.TeamUp.Resumes.DAL
{
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class HealthRepository : IHealthRepository
    {
        public Health GetHealth()
        {
            return new Health { Message = "I'm resumes-api and I'm alive and running! ;)" };
        }

        public Health PostHealth()
        {
            return new Health { Message = "I'm resumes-create-api and I'm alive and running! ;)" };
        }
    }
}
