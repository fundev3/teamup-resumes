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

    public class GetResumesBySkillCoreTests
    {
        private readonly Mock<IResumesRepository> mockRepository;
        private readonly ResumesService resumesService;

        public GetResumesBySkillCoreTests()
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
                            Name = "C#"
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
        public void GetResumesBySkill_NoItems_EmptyResult()
        {
            // Arrange
            var stubEmptyResumeList = new List<Resume>();
            this.mockRepository.Setup(repository => repository.GetBySkill("C#")).Returns(stubEmptyResumeList);

            // Act
            var result = this.resumesService.GetBySkill("C#");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumesBySkill_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetBySkill("C#")).Returns(GetTestResumes().ToList());

            // Act
            var result = this.resumesService.GetBySkill("C#");

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetResumesBySkill_NotExist_Skill()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetBySkill("VB actions")).Throws(new ResumesException(ResumesErrors.NotFound));

            // Assert
            Assert.Throws<ResumesException>(() => this.resumesService.GetBySkill("VB actions"));
        }
    }
}
