namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;

    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public Skill[] GetSkills(string name)
        {
            try
            {
                var skills = this.skillsRepository.GetSkills(name);
                if (skills.Count() == 0)
                {
                    throw new ResumeException(ErrorsTypes.NotFoundError, new Exception());
                }

                return skills.ToArray();
            }
            catch (ResumeException ex)
            {
                throw ex;
            }
        }
    }
}
