namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesRepository : IResumesRepository
    {
        private static List<Resume> resumes = new List<Resume>();

        public List<Resume> GetResumes()
        {
            return resumes;
        }
    }
}
