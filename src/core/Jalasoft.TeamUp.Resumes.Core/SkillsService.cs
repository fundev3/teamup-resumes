namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;

    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public Skill[] GetSkills(string name)
        {
            var skills = this.skillsRepository.GetSkills(name);
            if (skills != null)
            {
                return skills.ToArray();
            }
            else
            {
                throw new ResumesException(ResumesErrors.NotFound);
            }
        }
    }
}
