namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsService
    {
        public Skill[] GetSkills(string name);
    }
}