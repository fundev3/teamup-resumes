﻿namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesService
    {
        public Resume GetResume(Guid id);
    }
}
