namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository<Skill> skillsRepository;

        public SkillsService(IRepository<Skill> skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public Skill GetSkill(Guid id)
        {
            return this.skillsRepository.GetById(id);
        }

        public Skill[] GetSkills()
        {
            return this.skillsRepository.GetAll().ToArray();
        }

        public Guid GetSkillByParameter(string skill)
        {
            return this.skillsRepository.GetSkillByParameter(skill);
        }

        public Skill PostSkills(Skill skill)
        {
            return this.skillsRepository.Add(skill);
        }
    }
}
