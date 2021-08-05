namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsService
    {
        public Skill GetSkill(Guid id);

        Skill[] GetSkills();

        Skill PostSkills(Skill skill);

        Guid GetSkillByParameter(string skill);
    }
}