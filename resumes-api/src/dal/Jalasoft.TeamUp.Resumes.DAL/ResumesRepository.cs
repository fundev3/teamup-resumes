namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesRepository : IResumesRepository
    {
        private static IEnumerable<Resume> resumes = new List<Resume>();

        public IEnumerable<Resume> GetResumes()
        {
            return resumes;
        }

        public Resume PostResumes(Resume resume)
        {
            resumes = resumes.Append(resume);
            return resume;
        }
    }
}
