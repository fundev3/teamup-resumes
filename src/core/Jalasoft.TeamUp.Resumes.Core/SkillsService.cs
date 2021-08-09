﻿namespace Jalasoft.TeamUp.Resumes.Core
{
    using System.Collections.Generic;
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

        public IEnumerable<Skill> GetSkills(string name)
        {
            return this.skillsRepository.GetSkills(name);
        }
    }
}
