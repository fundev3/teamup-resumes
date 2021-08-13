﻿namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumeSQLRepository : IRepository<Resume>
    {
        IEnumerable<Skill> AddSkills(IEnumerable<Skill> skills);
    }
}
