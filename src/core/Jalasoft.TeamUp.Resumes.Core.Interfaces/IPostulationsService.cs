namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IPostulationsService
    {
        Postulation PostPostulation(Postulation postulation);

        Postulation[] GetPostulations(string value);
    }
}
