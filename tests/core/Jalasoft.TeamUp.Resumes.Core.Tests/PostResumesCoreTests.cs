namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PostResumesCoreTests
    {
        private readonly ResumesService resumeService;
        private readonly Mock<IResumesInMemoryRepository> mockRepository;

        public PostResumesCoreTests()
        {
            this.mockRepository = new Mock<IResumesInMemoryRepository>();
            this.resumeService = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void PostResumes_ValidResume_Resume()
        {
            var stubResume = new Resume
            {
                Id = 1,
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
                            Id = 1,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "API"
                        }
                    },
                CreationDate = DateTime.Now.AddDays(-10),
                LastUpdate = DateTime.Now
            };

            this.mockRepository.Setup(repository => repository.Add(stubResume)).Returns(new Resume());
            var result = this.resumeService.PostResumes(stubResume);
            Assert.IsType<Resume>(result);
        }

        [Fact]
        public void PostResume_InvalidResume_ValidationException()
        {
            var stubResume = new Resume()
            {
                Id = 2,
                Title = string.Empty,
                PersonalInformation = new Person()
                {
                    FirstName = "Juan Jose",
                    LastName = "Jimenez Javier",
                    Birthdate = new DateTime(1995, 2, 2),
                    Picture = "s.jpg"
                },
                Contact = new Contact()
                {
                    Email = "Jose Ecos",
                    Direction = "Calle Bolivar y Presidente Montes",
                    Phone = 75422166
                },
                Summary = "Juan Jose Sumary",
                Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = 1,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "API"
                        }
                    },
                CreationDate = DateTime.Now.AddDays(-10),
                LastUpdate = DateTime.Now
            };
            this.mockRepository.Setup(repository => repository.Add(stubResume)).Returns(new Resume());

            Assert.Throws<FluentValidation.ValidationException>(() => this.resumeService.PostResumes(stubResume));
        }
    }
}