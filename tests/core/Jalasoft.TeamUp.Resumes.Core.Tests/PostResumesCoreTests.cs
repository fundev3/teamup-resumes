namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PostResumesCoreTests
    {
        private readonly ResumesService resumeService;
        private readonly Mock<IRepository<Resume>> mockRepository;

        public PostResumesCoreTests()
        {
            this.mockRepository = new Mock<IRepository<Resume>>();
            this.resumeService = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void PostResumes_Returns_OkObjectResult()
        {
            var stubResume = new Resume
            {
                Id = new Guid("dd05d77a-ca64-401a-be39-8e1ea84e2f83"),
                Title = "My Custom Title",
                Person = new Person
                {
                    FirstName = "Rodrigo",
                    LastName = "Baldivieso",
                    Birthdate = new DateTime(1995, 1, 1),
                    Picture = "?"
                },
                Contact = new Contact
                {
                    Address = "Tarija Av.",
                    Email = "rodrigo.baldivieso@fundacion-jala.org",
                    Phone = 77669911
                },
                Summary = "Rodrigo's summary",
                Skills = new List<Skill>()
                    {
                        new Skill
                        {
                            Id = 9,
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = 10,
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
