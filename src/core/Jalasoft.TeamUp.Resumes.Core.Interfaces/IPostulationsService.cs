namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsService
    {
        Postulation PostPostulation(Postulation postulation);

        Postulation GetPostulation(int id);

        Postulation[] GetPostulations();

        Postulation[] GetPostulationsByProjectId(string projectId);
    }
}
