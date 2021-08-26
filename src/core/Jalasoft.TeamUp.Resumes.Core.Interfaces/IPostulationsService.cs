namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsService
    {
        Postulation PostPostulation(Postulation postulation);

        Postulation[] GetPostulations(int? resumeId);

        Postulation GetPostulation(int id);

        Postulation[] GetPostulationsByProjectId(string projectId);
    }
}
