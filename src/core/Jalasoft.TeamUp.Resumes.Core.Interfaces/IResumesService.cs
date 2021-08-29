namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesService
    {
        public Resume GetResume(int id);

        Resume[] GetResumes(string value);

        Resume PostResumes(Resume resume);

        Resume UpdateResumeSkill(int idResume, Skill[] skills);

        Resume[] GetByName(string name);
    }
}