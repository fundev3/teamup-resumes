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
        private readonly ISkillsInMemoryRepository skillsRepository;

        public SkillsService(ISkillsInMemoryRepository skillsRepository)
        {
            this.skillsRepository = skillsRepository;
        }

        public Skill[] GetSkills(string name)
        {
            return (Skill[])this.skillsRepository.GetSkills(name);
        }
    }
}
