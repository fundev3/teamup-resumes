namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ISkillsService
    {
        Skill[] GetSkills(string name);
    }
}