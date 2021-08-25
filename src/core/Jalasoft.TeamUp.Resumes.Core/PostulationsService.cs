namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PostulationsService : IPostulationsService
    {
        private readonly IPostulationsRepository postulationsRepository;

        public PostulationsService(IPostulationsRepository resumesRepository)
        {
            this.postulationsRepository = resumesRepository;
        }

        public Postulation GetPostulation(int id)
        {
            throw new NotImplementedException();
        }

        public Postulation[] GetPostulations()
        {
            throw new NotImplementedException();
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
