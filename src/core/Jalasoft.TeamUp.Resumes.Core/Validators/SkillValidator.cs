namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using System.Collections.Generic;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillValidator : AbstractValidator<Skill[]>
    {
        public SkillValidator()
        {
            this.RuleForEach(skills => skills).NotNull().ChildRules(orders =>
            {
                orders.RuleFor(skill => skill.Name).NotNull();
                orders.RuleFor(skill => skill.Name).NotEmpty();
                orders.RuleFor(skill => skill.Name).MaximumLength(50);
                orders.RuleFor(skill => skill.Id).NotNull();
                orders.RuleFor(skill => skill.Id).NotEmpty();
                orders.RuleFor(skill => skill.Id).MaximumLength(50);
            });
        }
    }
}
