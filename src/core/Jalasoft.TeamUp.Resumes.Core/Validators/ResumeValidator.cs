namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumeValidator : AbstractValidator<Resume>
    {
        public ResumeValidator()
        {
            this.RuleFor(x => x.Title)
                .NotEmpty();
            this.RuleFor(x => x.Summary)
                .NotEmpty();
            this.RuleFor(x => x.PersonalInformation)
                .NotEmpty();
        }
    }
}
