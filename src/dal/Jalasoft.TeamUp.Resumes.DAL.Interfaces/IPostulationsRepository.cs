namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsRepository : IRepository<Postulation>
    {
        IEnumerable<Postulation> GetPostulationsByResumeId(string value);

        IEnumerable<Postulation> GetAllByProjectId(string projectId);
    }
}
