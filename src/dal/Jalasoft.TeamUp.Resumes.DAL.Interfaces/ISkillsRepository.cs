namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsRepository
    {
        public IEnumerable<Skill> GetSkills(string name);
    }
}
