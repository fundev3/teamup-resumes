namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesSkillsService : IResumesSkillsService
    {
        private readonly IResumeSkillSQLRepository resumesSkillsRepository;

        public ResumesSkillsService(IResumeSkillSQLRepository resumesSkillsRepository)
        {
            this.resumesSkillsRepository = resumesSkillsRepository;
        }

        public ResumeSkill[] AddResumeSkills(IEnumerable<Skill> skills, Guid idResume)
        {
            return this.resumesSkillsRepository.AddResumesSkills(ResumeSkill.GetResumeSkills(skills, idResume)).ToArray();
        }
    }
}
