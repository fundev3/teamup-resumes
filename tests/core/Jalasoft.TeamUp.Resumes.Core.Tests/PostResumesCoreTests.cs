namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PostResumesCoreTests
    {
        private readonly ResumesService resumeService;
        private readonly Mock<IResumesRepository> mockRepository;

        public PostResumesCoreTests()
        {
            this.mockRepository = new Mock<IResumesRepository>();
            this.resumeService = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void PostResumes_Returns_OkObjectResult()
        {
            var stubResume = new Resume
            {
                Id = new Guid("dd05d77a-ca64-401a-be39-8e1ea84e2f83"),
                Title = "My Custom Title",
                PersonalInformation = new Person
                {
                    FirstName = "Rodrigo",
                    LastName = "Baldivieso",
                    Birthdate = new DateTime(1995, 1, 1),
                    Picture = "?"
                },
                Contact = new Contact
                {
                    Direction = "Tarija Av.",
                    Email = "rodrigo.baldivieso@fundacion-jala.org",
                    Phone = 77669911
                },
                Summary = "Rodrigo's summary",
                Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = new Guid("0947a444-09c6-4281-894a-5e7a4acc38eb"),
                            NameSkill = "API"
                        }
                    },
                CreationDate = DateTime.Now.AddDays(-10),
                LastUpdate = DateTime.Now
            };

            this.mockRepository.Setup(repository => repository.Add(stubResume)).Returns(new Resume());
            var result = this.resumeService.PostResumes(stubResume);
            Assert.IsType<Resume>(result);
        }
    }
}
