namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesInMemoryRepository : IRepository<Resume>
    {
        IEnumerable<Skill> AddSkills(Skill[] skills);

        Skill SearchSkill(string emsiId);
    }
}
