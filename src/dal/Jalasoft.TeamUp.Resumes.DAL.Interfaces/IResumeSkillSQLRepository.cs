namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumeSkillSQLRepository
    {
        IEnumerable<ResumeSkill> AddResumesSkills(IEnumerable<ResumeSkill> resumeSkills);
    }
}
