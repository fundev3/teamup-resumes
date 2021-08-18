﻿namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesService
    {
        public Resume GetResume(int id);

        Resume[] GetResumes();

        Resume PostResumes(Resume resume);

        Resume[] GetByName(string name);
    }
}