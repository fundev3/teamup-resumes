namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PostResumeSkillsCoreTests
    {
        private readonly ResumesSkillsService resumesSkillsService;
        private readonly Mock<IResumeSkillSQLRepository> mockResumeSkillsRepository;

        public PostResumeSkillsCoreTests()
        {
            this.mockResumeSkillsRepository = new Mock<IResumeSkillSQLRepository>();
            this.resumesSkillsService = new ResumesSkillsService(this.mockResumeSkillsRepository.Object);
        }

        [Fact]
        public void PostResumes_Returns_OkObjectResult()
        {
            var stubResumesSkills = new ResumeSkill[]
            {
                new ResumeSkill
                {
                    ResumeId = Guid.NewGuid(),
                    SkillId = 1
                },
                new ResumeSkill
                {
                    ResumeId = Guid.NewGuid(),
                    SkillId = 2
                }
            };

            var stubSkills = new Skill[]
            {
                new Skill
                {
                    Name = "C#"
                },
                new Skill
                {
                    Name = "React"
                }
            };

            this.mockResumeSkillsRepository.Setup(repository => repository.AddResumesSkills(stubResumesSkills)).Returns(new List<ResumeSkill>() { new ResumeSkill() }.ToArray());
            var result = this.resumesSkillsService.AddResumeSkills(stubSkills, Guid.NewGuid());
            Assert.IsType<ResumeSkill[]>(result);
        }
    }
}
