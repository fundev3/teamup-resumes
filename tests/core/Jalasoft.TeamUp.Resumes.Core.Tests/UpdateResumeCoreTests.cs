namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation;
    using FluentValidation.Results;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class UpdateResumeCoreTests
    {
        private readonly ResumesService resumesService;

        private readonly Mock<IResumesRepository> mockResumeRepository;

        public UpdateResumeCoreTests()
        {
            this.mockResumeRepository = new Mock<IResumesRepository>();
            this.resumesService = new ResumesService(this.mockResumeRepository.Object);
        }

        public List<Skill> GetSkills()
        {
            var stubSkill = new List<Skill>()
            {
                new Skill
                {
                    Id = "1",
                    Name = "c#"
                },
                new Skill
                {
                    Id = "2",
                    Name = "devopsTest"
                }
            };

            return stubSkill;
        }

        [Fact]
        public void UpdateResume_ExistentId_Resume()
        {
            this.mockResumeRepository.Setup(repository => repository.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(this.GetSkills());
            var result = this.resumesService.UpdateResumeSkill(2, this.GetSkills().ToArray());
            Assert.IsType<Skill[]>(result.ToArray());
        }

        [Fact]
        public void UpdateResume_UnexistentId_EmptyList()
        {
            this.mockResumeRepository.Setup(repository => repository.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(new Skill[0]);
            var result = this.resumesService.UpdateResumeSkill(7, this.GetSkills().ToArray());
            Assert.IsType<Skill[]>(result.ToArray());
        }
    }
}
