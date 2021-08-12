namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class ResumesServiceTests
    {
        private readonly Mock<IRepository<Resume>> mockRepository;
        private readonly ResumesService resumesService;

        public ResumesServiceTests()
        {
            this.mockRepository = new Mock<IRepository<Resume>>();
            this.resumesService = new ResumesService(this.mockRepository.Object);
        }

        public static IEnumerable<Resume> GetTestResumes()
        {
            var resumes = new List<Resume>
            {
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    Title = "Guido Castro",
                    Contact = new Contact
                    {
                        Address = "15 street",
                        Email = "guido.castro@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Person = new Person
                    {
                        FirstName = "Guido",
                        LastName = "Castro",
                        Birthdate = DateTime.Now,
                        Picture = "picture.jpg"
                    },
                    Summary = "I'm a great developer :-D",
                    Skills = new List<Skill>()
                    {
                        new Skill
                        {
                            Id = 5,
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = 6,
                            NameSkill = "Javascript"
                        },
                    }
                },
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    Title = "Marcelo Ruiz",
                    Contact = new Contact
                    {
                        Address = "16 street",
                        Email = "marcelo.ruiz@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Person = new Person
                    {
                        FirstName = "Marcelo",
                        LastName = "Ruiz",
                        Birthdate = DateTime.Now,
                        Picture = "picture.jpg"
                    },
                    Summary = "I'm a great ux :-D",
                    Skills = new List<Skill>()
                    {
                        new Skill
                        {
                            Id = 7,
                            NameSkill = "Figma"
                        },
                        new Skill
                        {
                            Id = 8,
                            NameSkill = "HTML"
                        },
                    }
                }
            };
            return resumes;
        }

        [Fact]
        public void GetResumes_Returns_EmptyResult()
        {
            // Arrange
            var stubEmptyResumeList = new List<Resume>();
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(stubEmptyResumeList);

            // Act
            var result = this.resumesService.GetResumes();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetResumes();

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetResume_Returns_SingleResume()
        {
            // Arrange
            var stubResume = new Resume { Id = Guid.NewGuid() };
            this.mockRepository.Setup(repository => repository.GetById(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(stubResume);

            // Act
            var result = this.resumesService.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            Assert.NotNull(result);
        }
    }
}
