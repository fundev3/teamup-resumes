namespace Jalasoft.TeamUp.Resumes.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesService : IResumesService
    {
        private readonly IResumesRepository resumesRepository;

        public ResumesService(IResumesRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume GetResume(int id)
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
            ResumeValidator validator = new ResumeValidator();
            validator.ValidateAndThrow(resume);
            var result = this.resumesRepository.Add(resume);
            return result;
        }

        public IEnumerable<Skill> UpdateResumeSkill(int idResume, Skill[] skills)
        {
            var skillValidator = new SkillValidator();
            var existResumeValidator = new ExistResumeValidator(this.resumesRepository);

            skillValidator.ValidateAndThrow(skills);
            existResumeValidator.ValidateAndThrow(idResume);

            return this.resumesRepository.UpdateResumeSkill(idResume, skills).ToList();
        }
    }
}
