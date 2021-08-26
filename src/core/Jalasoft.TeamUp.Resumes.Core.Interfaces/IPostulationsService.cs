namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsService
    {
        Postulation PostPostulation(Postulation postulation);

        Postulation GetPostulation(int id);

        Postulation[] GetPostulations(int? resumeId);

        Postulation[] GetPostulationsByProjectId(string projectId);

        Postulation PatchPostulation(Postulation postulation);
    }
}
