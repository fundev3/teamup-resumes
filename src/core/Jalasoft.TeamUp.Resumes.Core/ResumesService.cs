namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;

    public class ResumesService : IResumesService
    {
        private readonly IRepository<Resume> resumesRepository;

        public ResumesService(IRepository<Resume> resumesRepository)
        {
            this.resumesRepository = resumesRepository;
        }

        public Resume GetResume(int id)
        {
            try
            {
                var resume = this.resumesRepository.GetById(id);
                if (resume == null)
                {
                    throw new ResumeException(ErrorsTypes.NotFoundError, new Exception());
                }

                return resume;
            }
            catch (ResumeException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }

        public Resume[] GetResumes()
        {
            try
            {
                return this.resumesRepository.GetAll().ToArray();
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }

        public Resume PostResumes(Resume resume)
        {
            try
            {
                return this.resumesRepository.Add(resume);
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }
    }
}
