namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // using FluentValidation.Results;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;

    public class ResumesService : IResumesService
    {
        private readonly IResumeSQLRepository resumesRepository;

        public ResumesService(IResumeSQLRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume GetResume(Guid id)
        {
            try
            {
                var resume = this.resumesRepository.GetById(id);
                if (resume == null)
                {
                    throw new ResumeException(ErrorsTypes.NotFoundError, new Exception());
                }

                return resume;
            }
            catch (ResumeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }

        public Resume[] GetResumes()
        {
            try
            {
                return this.resumesRepository.GetAll().ToArray();
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }

        public Resume PostResumes(Resume resume)
        {
            try
            {
                return this.resumesRepository.Add(resume);
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }

        public Resume UpdateResume(Resume resume)
        {
            // SkillValidator skillValidator = new SkillValidator();
            // var skillsForAdd = new List<Skill>();
            // foreach (var skill in resume.Skills)
            // {
            //    ValidationResult v = skillValidator.Validate(skill);
            //    foreach (var e in v.Errors)
            //    {
            //        if (e.ErrorCode == "Skill not found.")
            //        {
            //            skillsForAdd.Add(skill);
            //        }
            //    }
            // }
            // this.resumesRepository.AddSkills(skillsForAdd).ToArray();
            return this.resumesRepository.Update(resume.Id, resume);
        }
    }
}
