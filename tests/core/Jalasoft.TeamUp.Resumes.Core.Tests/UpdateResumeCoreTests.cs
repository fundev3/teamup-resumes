namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System.Collections.Generic;
    using System.Linq;
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

        public static List<Skill> GetResume()
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
            this.mockResumeRepository.Setup(repository => repository.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(UpdateResumeCoreTests.GetResume());
            var result = this.resumesService.UpdateResumeSkill(1, UpdateResumeCoreTests.GetResume().ToArray());
            Assert.IsType<Skill[]>(result.ToArray());
        }
    }
}
