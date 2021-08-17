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
        private readonly IResumeSQLRepository resumesRepository;

        public ResumesService(IResumeSQLRepository resumesRepository)
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

        public Resume UpdateResume(Resume resume)
        {
            SkillValidator skillValidator = new SkillValidator();
            var skillsForAdd = new List<Skill>();
            foreach (var skill in resume.Skills)
            {
                ValidationResult validation = skillValidator.Validate(skill);
                foreach (var e in validation.Errors)
                {
                    if (e.ErrorCode == "Skill not found.")
                    {
                        skillsForAdd.Add(skill);
                    }
                }
            }

            this.resumesRepository.AddSkills(skillsForAdd);

            return this.resumesRepository.Update(resume.Id, resume);
        }
    }
}
