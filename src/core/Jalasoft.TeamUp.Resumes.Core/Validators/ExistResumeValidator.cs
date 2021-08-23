namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ExistResumeValidator : AbstractValidator<int>
    {
        private readonly IResumesRepository resumesRepository;

        public ExistResumeValidator(IResumesRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
            this.RuleFor(x => x)
                .Must(this.ExistResume).WithErrorCode("404").WithMessage("Resume with idResume doesn't exist.");
        }

        private bool ExistResume(int id)
        {
            var result = this.resumesRepository.GetById(id);
            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
