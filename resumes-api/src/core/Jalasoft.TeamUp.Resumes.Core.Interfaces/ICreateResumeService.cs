namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using Jalasoft.TeamUp.Resumes.Models;

    public interface ICreateResumeService
    {
        Resume[] PostResume();
    }
}
