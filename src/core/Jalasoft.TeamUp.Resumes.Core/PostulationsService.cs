namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PostulationsService : IPostulationsService
    {
        private readonly IPostulationsRepository postulationsRepository;

        public PostulationsService(IPostulationsRepository postulationsRepository)
        {
            this.postulationsRepository = postulationsRepository;
        }

        public Postulation[] GetPostulations(int? resumeId)
        {
            return this.postulationsRepository.GetPostulationsByResumeId(resumeId).ToArray();
        }

        public Postulation[] GetPostulationsByProjectId(string projectId)
        {
            return this.postulationsRepository.GetAllByProjectId(projectId).ToArray();
        }

        public Postulation PatchPostulation(Postulation postulation)
        {
            return this.postulationsRepository.UpdatePostulation(postulation);
        }

        public Postulation PostPostulation(Postulation postulation)
        {
            var postulationValidator = new PostulationValidator();
            postulationValidator.ValidateAndThrow(postulation);

            var result = this.postulationsRepository.AddPostulation(postulation);
            return result;
        }
    }
}
