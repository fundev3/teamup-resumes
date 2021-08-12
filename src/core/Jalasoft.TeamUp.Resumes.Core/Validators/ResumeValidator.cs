namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using System;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumeValidator : AbstractValidator<Resume>
    {
        public ResumeValidator()
        {
            this.RuleFor(resume => resume.Id)
                .NotEmpty()
                .NotNull();

            this.RuleFor(resume => resume.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(160);

            this.RuleFor(resume => resume.Summary)
                .NotEmpty()
                .NotNull()
                .MaximumLength(160);
        }
    }
}
