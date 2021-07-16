namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class CreateResumeRepository : ICreateResumeRepository
    {
        private static IEnumerable<Resume> resume = new List<Resume>();

        public IEnumerable<Resume> PostResume()
        {
            return resume;
        }
    }
}
