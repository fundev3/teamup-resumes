﻿namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesRepository
    {
        public Resume GetResume(Guid id);

        IEnumerable<Resume> GetResumes();

        Resume PostResumes(Resume resume);
    }
}
