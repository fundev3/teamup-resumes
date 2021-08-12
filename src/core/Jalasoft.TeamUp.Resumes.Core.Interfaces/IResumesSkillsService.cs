namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesSkillsService
    {
        ResumeSkill[] AddResumeSkills(IEnumerable<Skill> skills, Guid idResume);
    }
}
