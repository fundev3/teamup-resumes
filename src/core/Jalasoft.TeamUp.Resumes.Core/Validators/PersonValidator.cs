namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            this.RuleFor(x => x.FirstName)
                .NotEmpty();
            this.RuleFor(x => x.FirstName)
                .MaximumLength(50);
            this.RuleFor(x => x.LastName)
                .NotEmpty();
            this.RuleFor(x => x.LastName)
                .MaximumLength(50);
        }
    }
}
