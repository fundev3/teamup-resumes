namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationService
    {
        Postulation[] GetPostulationsByResumeId(int id);
    }
}
