namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class ResumesServiceTests
    {
        private readonly Mock<IResumesRepository> mockRepository;
        private readonly ResumesService resumesService;

        public ResumesServiceTests()
        {
            this.mockRepository = new Mock<IResumesRepository>();
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
                    Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            Name = "Javascript"
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
                    Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            Name = "Figma"
                        },
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            Name = "HTML"
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
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(stubEmptyResumeList);

            // Act
            var result = this.resumesService.GetResumes();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(GetTestResumes());

            // Act
            var result = this.resumesService.GetResumes();

            // Assert
            Assert.Equal(2, result.Length);
        }
    }
}
