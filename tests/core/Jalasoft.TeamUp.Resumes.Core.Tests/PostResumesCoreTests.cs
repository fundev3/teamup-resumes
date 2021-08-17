namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core;
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
        public void PostResumes_ValidResume_Resume()
        {
            var stubResume = new Resume
            {
                Id = 1,
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
                            Id = "KS120P86XDXZJT3B7KVJ",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "KS120P86XDXZJT3B7KVJ",
                            Name = "Javascript"
                        },
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
                Id = 1,
                Title = string.Empty,
                Person = new Person()
                {
                    FirstName = "Juan Jose",
                    LastName = "Jimenez Javier",
                    Birthdate = new DateTime(1995, 2, 2),
                    Picture = "s.jpg"
                },
                Contact = new Contact()
                {
                    Email = "Jose Ecos",
                    Address = "Calle Bolivar y Presidente Montes",
                    Phone = 75422166
                },
                Summary = "Juan Jose Sumary",
                Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Id = 2,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 3,
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