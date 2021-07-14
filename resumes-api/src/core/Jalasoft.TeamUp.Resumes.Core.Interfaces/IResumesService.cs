namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IResumesService
    {
        List<Resume> GetResumes();
    }
}
