namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
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
            {
                var resume = this.resumesRepository.GetById(id);
                return resume;
            }
        }

        public Resume[] GetResumes()
        {
            var resumes = this.resumesRepository.GetAll().ToArray();
            return resumes;
        }

        public Resume PostResumes(Resume resume)
        {
            resume.Id = Guid.NewGuid();
            ResumeValidator validator = new ResumeValidator();
            validator.ValidateAndThrow(resume);
            var result = this.resumesRepository.Add(resume);
            return result;
        }
    }
}
