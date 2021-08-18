namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Moq;
    using Xunit;

    public class ResumesServiceTests
    {
        private readonly Mock<IResumesInMemoryRepository> mockRepository;
        private readonly ResumesService resumesService;

        public ResumesServiceTests()
        {
            this.mockRepository = new Mock<IResumesInMemoryRepository>();
            this.resumesService = new ResumesService(this.mockRepository.Object);
        }

        public static IEnumerable<Resume> GetTestResumes()
        {
            var resumes = new List<Resume>
            {
                new Resume()
                {
                    Id = 3,
                    Title = "Guido Castro",
                    Contact = new Contact
                    {
                        Direction = "15 street",
                        Email = "guido.castro@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    PersonalInformation = new Person
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
                            Id = 1,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "Javascript"
                        },
                    }
                },
                new Resume()
                {
                    Id = 4,
                    Title = "Marcelo Ruiz",
                    Contact = new Contact
                    {
                        Direction = "16 street",
                        Email = "marcelo.ruiz@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    PersonalInformation = new Person
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
                            Id = 1,
                            Name = "Figma"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "HTML"
                        },
                    }
                }
            };
            return resumes;
        }

        [Fact]
        public void GetResumes_NoItems_EmptyResult()
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
        public void GetResumes_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetResumes();

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetResume_ValidId_SingleResume()
        {
            // Arrange
            var stubResume = new Resume { Id = 1 };
            this.mockRepository.Setup(repository => repository.GetById(1)).Returns(stubResume);

            // Act
            var result = this.resumesService.GetResume(1);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetResume_UnexistentID_ReturnNull()
        {
            Resume resp = null;

            // Arrange
            this.mockRepository.Setup(repository => repository.GetById(81)).Returns(resp);

            // Assert
            Assert.Null(this.resumesService.GetResume(81));
        }
    }
}