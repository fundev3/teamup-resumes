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
                .NotEmpty().NotNull();

            this.RuleFor(resume => resume.Title)
                .Length(3, 15)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty().NotNull();

            this.RuleFor(resume => resume.Summary)
                .NotEmpty().NotNull()
                .MaximumLength(160);
        }
    }
}
