namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesRepository : IResumesRepository
    {
        private static IEnumerable<Resume> resumes = new List<Resume>();

        public Resume GetResume(Guid id)
        {
            Resume result = resumes.Where(p => Guid.Equals(p.Id, id)).SingleOrDefault();
            return result;
        }
    }
}
