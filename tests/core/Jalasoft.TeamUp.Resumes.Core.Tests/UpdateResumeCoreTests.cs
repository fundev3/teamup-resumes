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

    public class UpdateResumeCoreTests
    {
        private readonly ResumesService resumesService;

        private readonly Mock<IResumesInMemoryRepository> mockResumeRepository;

        public UpdateResumeCoreTests()
        {
            this.mockResumeRepository = new Mock<IResumesInMemoryRepository>();
            this.resumesService = new ResumesService(this.mockResumeRepository.Object);
        }

        public static Resume GetResume()
        {
            var stubSkill = new Skill[]
            {
                new Skill
                {
                    Id = 1,
                    Name = "c#"
                },
                new Skill
                {
                    Id = 1,
                    Name = "devopsTest"
                }
            };

            var resume = new Resume()
            {
                Id = 1,
                Skills = stubSkill
            };

            return resume;
        }

        [Fact]
        public void UpdateResume_Returns_Resume()
        {
            this.mockResumeRepository.Setup(repository => repository.Update(It.IsAny<Resume>())).Returns(UpdateResumeCoreTests.GetResume());
            var result = this.resumesService.UpdateResume(UpdateResumeCoreTests.GetResume());
            Assert.IsType<Resume>(result);
            Assert.IsType<Skill[]>(result.Skills);
        }
    }
}
