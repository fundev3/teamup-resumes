namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils;

    public class ResumesService : IResumesService
    {
        private readonly IRepository<Resume> resumesRepository;

        public ResumesService(IRepository<Resume> resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume GetResume(Guid id)
        {
            try
            {
                var resume = this.resumesRepository.GetById(id);
                if (resume == null)
                {
                    throw new ResumeException(404);
                }

                return resume;
            }
            catch (ResumeException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new ResumeException(500);
            }
        }

        public Resume[] GetResumes()
        {
            try
            {
                return this.resumesRepository.GetAll().ToArray();
            }
            catch (Exception)
            {
                throw new ResumeException(500);
            }
        }

        public Resume PostResumes(Resume resume)
        {
            try
            {
                return this.resumesRepository.Add(resume);
            }
            catch (Exception)
            {
                throw new ResumeException(500);
            }
        }
    }
}
