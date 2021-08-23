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
                .NotNull()
                .WithErrorCode("400");

            this.RuleFor(resume => resume.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(160)
                .WithErrorCode("400");

            this.RuleFor(resume => resume.Summary)
                .NotEmpty()
                .NotNull()
                .MaximumLength(160)
                .WithErrorCode("400");
        }
    }
}
