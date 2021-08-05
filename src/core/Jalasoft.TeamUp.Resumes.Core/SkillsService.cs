namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository skillsRepository;

        public SkillsService(ISkillsRepository skillsRepository)
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

        public Skill[] GetSkillByName(string skill)
        {
            return (Skill[])this.skillsRepository.GetSkillsByName(skill);
        }

        public Skill PostSkills(Skill skill)
        {
            return this.skillsRepository.Add(skill);
        }
    }
}
