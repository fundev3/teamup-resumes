﻿namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesRepository : IRepository<Resume>
    {
        IEnumerable<Skill> UpdateResumeSkill(int idResume, Skill[] skills);
    }
}
