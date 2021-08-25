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
            if (string.IsNullOrEmpty(value))
            {
                return this.postulationsRepository.GetPostulationsById(null).ToArray();
            }
            else
            {
                return this.postulationsRepository.GetPostulationsById(value).ToArray();
            }
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
