namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesService : IResumesService
    {
        private readonly IRepository<Resume> resumesRepository;

        public ResumesService(IRepository<Resume> resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume GetResume(Guid id)
        {
            return this.resumesRepository.GetById(id);
        }

        public Resume[] GetResumes()
        {
            return this.resumesRepository.GetAll().ToArray();
        }

        public Resume PostResumes(Resume resume)
        {
            return this.resumesRepository.Add(resume);
        }

        public Resume[] GetSkills()
        {
            return this.resumesRepository.GetAll().ToArray();
        }
    }
}
