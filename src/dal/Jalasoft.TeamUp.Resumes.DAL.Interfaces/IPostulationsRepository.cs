namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsRepository
    {
        Postulation AddPostulation(Postulation postulation);

        IEnumerable<Postulation> GetPostulationsByResumeId(int? resumeId);

        IEnumerable<Postulation> GetAllByProjectId(string projectId);

        Postulation UpdatePostulation(Postulation updateObject);
    }
}
