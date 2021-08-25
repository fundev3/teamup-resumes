namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesRepository : IRepository<Resume>
    {
        IEnumerable<Resume> GetByName(string name);

        IEnumerable<Skill> UpdateResumeSkill(int idResume, Skill[] skills);

        IEnumerable<Resume> GetBySkill(string skill);
    }
}
