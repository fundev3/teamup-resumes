namespace Jalasoft.TeamUp.Resumes.Core
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesService : IResumesService
    {
        private readonly IResumesRepository resumesRepository;

        public ResumesService(IResumesRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public List<Resume> GetResumes()
        {
            return this.resumesRepository.GetResumes();
        }
    }
}
