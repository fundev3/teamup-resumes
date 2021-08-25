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

        public Postulation[] GetPostulations(string value)
        {
                return this.postulationsRepository.GetPostulationsResumeId(value).ToArray();
        }

        public Postulation[] GetPostulationsByProjectId(string projectId)
        {
            var postulations = this.postulationsRepository.GetAllByProjectId(projectId);
            return postulations.ToArray();
        }

        public Postulation PostPostulation(Postulation postulation)
        {
            var postulationValidator = new PostulationValidator();
            postulationValidator.ValidateAndThrow(postulation);

            var result = this.postulationsRepository.Add(postulation);
            return result;
        }
    }
}
