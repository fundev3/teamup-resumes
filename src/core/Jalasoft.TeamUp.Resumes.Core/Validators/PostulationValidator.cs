namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PostulationValidator : AbstractValidator<Postulation>
    {
        public PostulationValidator()
        {
            this.RuleFor(postulation => postulation.ProjectId)
                .NotEmpty();

            this.RuleFor(postulation => postulation.ResumeId)
                .NotEmpty();

            this.RuleFor(postulation => postulation.ProjectName)
                .Length(3, 15)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty();

            this.RuleFor(postulation => postulation.ResumeName)
                .Length(3, 15)
                .Matches("^[a-zñ A-ZÑ]+$")
                .NotEmpty();

            this.RuleFor(postulation => postulation.State)
                .Matches("^(Applied|Rejected|Accepted|Canceled)$");

            this.RuleFor(postulation => postulation.Picture)
                .Matches("[^\\s]+(.*?)\\.(jpg|jpeg|png|JPG|JPEG|PNG)$");
        }
    }
}
