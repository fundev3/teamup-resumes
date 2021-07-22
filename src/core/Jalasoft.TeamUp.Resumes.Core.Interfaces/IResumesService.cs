namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesService
    {
        Resume[] GetResumes();

        Resume PostResumes(Resume resume);
    }
}
