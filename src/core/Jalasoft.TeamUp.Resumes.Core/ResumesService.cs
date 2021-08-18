namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using FluentValidation.Results;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesService : IResumesService
    {
        private readonly IResumesInMemoryRepository resumesRepository;

        public ResumesService(IResumesInMemoryRepository resumesRepository)
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

        public Resume UpdateResume(Resume resume)
        {
            SkillValidator skillValidator = new SkillValidator(this.resumesRepository);
            var skillsForAdd = new List<Skill>();
            foreach (var skill in resume.Skills)
            {
                try
                {
                    skillValidator.ValidateAndThrow(skill);
                }
                catch (ValidationException)
                {
                    skillsForAdd.Add(skill);
                }
            }

            this.resumesRepository.AddSkills(skillsForAdd.ToArray());

            return this.resumesRepository.Update(resume);
        }
    }
}
