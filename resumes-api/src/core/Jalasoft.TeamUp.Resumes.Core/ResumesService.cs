﻿namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesService : IResumesService
    {
        private readonly IResumesRepository resumesRepository;

        public ResumesService(IResumesRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume[] GetResumes()
        {
            return this.resumesRepository.GetResumes().ToArray();
        }

        public Resume PostResumes(Resume resume)
        {
            resume.Id = Guid.NewGuid();
            return this.resumesRepository.PostResumes(resume);
        }
    }
}
