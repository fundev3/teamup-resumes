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
        private readonly Mock<IResumeSQLRepository> mockRepository;

        public PostResumesCoreTests()
        {
            this.mockRepository = new Mock<IResumeSQLRepository>();
            this.resumeService = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void PostResumes_ValidResume_Resume()
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
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "0947a444-09c6-4281-894a-5e7a4acc38eb",
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
                Id = Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"),
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
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "0947a444-09c6-4281-894a-5e7a4acc38eb",
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