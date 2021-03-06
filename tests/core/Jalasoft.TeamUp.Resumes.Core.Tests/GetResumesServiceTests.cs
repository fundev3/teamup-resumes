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

    public class GetResumesServiceTests
    {
        private readonly Mock<IResumesRepository> mockRepository;
        private readonly ResumesService resumesService;

        public GetResumesServiceTests()
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
                    Id = 1,
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
                            Id = "1",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "2",
                            Name = "Javascript"
                        },
                    }
                },
                new Resume()
                {
                    Id = 2,
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
                            Id = "1",
                            Name = "Figma"
                        },
                        new Skill
                        {
                            Id = "2",
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
            var result = this.resumesService.GetResumes(It.IsAny<string>());

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetResumes(It.IsAny<string>());

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
            this.mockRepository.Setup(repository => repository.GetById(1)).Returns(resp);

            // Assert
            Assert.Null(this.resumesService.GetResume(1));
        }

        [Fact]
        public void GetResumesByName_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetByName("Gustavo")).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetByName("Gustavo");

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetResumesByName_NoItems_EmptyResult()
        {
            // Arrange
            var stubEmptyResumeList = new List<Resume>();
            this.mockRepository.Setup(repository => repository.GetByName("Gustavo")).Returns(stubEmptyResumeList);

            // Act
            var result = this.resumesService.GetByName("Gustavo");

            // Assert
            Assert.Empty(result);
        }

        // -----------------------------------------
        [Fact]
        public void GetResumesBySkill_NoItems_EmptyResult()
        {
            // Arrange
            var stubEmptyResumeList = new List<Resume>();
            this.mockRepository.Setup(repository => repository.GetBySkill("C#")).Returns(stubEmptyResumeList);

            // Act
            var result = this.resumesService.GetResumes("C#");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumesBySkill_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetBySkill("C#")).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetResumes("C#");

            // Assert
            Assert.Equal(2, result.Length);
        }
    }
}