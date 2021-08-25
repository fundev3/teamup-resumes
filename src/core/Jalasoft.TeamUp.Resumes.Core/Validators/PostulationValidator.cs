namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PostulationValidator : AbstractValidator<Postulation>
    {
        public PostulationValidator()
        {
            this.RuleFor(postulation => postulation.ProjectId)
                .NotEmpty()
                .NotNull();

            this.RuleFor(postulation => postulation.ResumeId)
                .NotEmpty()
                .NotNull();

            this.RuleFor(postulation => postulation.ProjectName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            this.RuleFor(postulation => postulation.ResumeName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            this.RuleFor(postulation => postulation.Status)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);

            this.RuleFor(postulation => postulation.Picture)
                .NotEmpty()
                .NotNull();

            this.RuleFor(postulation => postulation.StartDate)
                .NotEmpty()
                .NotNull();

            this.RuleFor(postulation => postulation.ExpireDate)
                .NotEmpty()
                .NotNull();
        }
    }
}
