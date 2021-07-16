namespace Jalasoft.TeamUp.Resumes.Core
{
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class CreateResumeService : ICreateResumeService
    {
        private readonly ICreateResumeRepository createResumeRepository;

        public CreateResumeService(ICreateResumeRepository createResumeRepository)
        {
            this.createResumeRepository = createResumeRepository;
        }

        public Resume[] PostResume()
        {
            return this.createResumeRepository.PostResume().ToArray();
        }
    }
}
