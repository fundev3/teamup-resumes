namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillValidator : AbstractValidator<Skill>
    {
        private readonly IResumesInMemoryRepository resumesRepository;

        public SkillValidator(IResumesInMemoryRepository resumesRepository)
        {
            this.resumesRepository = resumesRepository;
            this.RuleFor(x => x)
                .Must(this.FoundSkill).WithErrorCode("Skill not found.");
        }

        private bool FoundSkill(Skill skill)
        {
            var result = this.resumesRepository.SearchSkill(skill.Id);
            if (result != null)
            {
                return true;
            }

            return false;
        }
    }
}
