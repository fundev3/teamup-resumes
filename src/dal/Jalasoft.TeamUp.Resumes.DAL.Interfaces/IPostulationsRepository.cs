﻿namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsRepository : IRepository<Postulation>
    {
        IEnumerable<Postulation> GetAllByProjectId(string projectId);
    }
}
