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

        public static List<Skill> GetSkills()
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
            this.mockResumeRepository.Setup(repository => repository.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(UpdateResumeCoreTests.GetSkills());
            this.mockResumeRepository.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(new Resume());
            var result = this.resumesService.UpdateResumeSkill(2, UpdateResumeCoreTests.GetSkills().ToArray());
            Assert.IsType<Skill[]>(result.ToArray());
        }

        [Fact]
        public void UpdateResume_UnexistentId_ValidationException()
        {
            Resume resume = null;
            var error = new ValidationFailure("Resume", "Object Doesn't exist");
            error.ErrorCode = "404";
            this.mockResumeRepository.Setup(repository => repository.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Throws(new ValidationException(new List<ValidationFailure>() { error }));
            this.mockResumeRepository.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(resume);
            Assert.Throws<ValidationException>(() => this.resumesService.UpdateResumeSkill(1, UpdateResumeCoreTests.GetSkills().ToArray()));
        }
    }
}
