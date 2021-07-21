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

        [Fact]
        public void GetResumes_Returns_EmptyResult()
        {
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(new List<Resume>());

            var result = this.resumesService.GetResumes();

            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(this.GetTestResumes());

            var result = this.resumesService.GetResumes();

            Assert.Equal(2, result.Length);
        }

        public IEnumerable<Resume> GetTestResumes()
        {
            var resumes = new List<Resume>
            {
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    Title = "Guido Castro",
                    Contact = new Contact
                    {
                        Direction = "15 street",
                        Email = "guido.castro@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    PersonalInformation = new PersonalInformation
                    {
                        FirstName = "Guido",
                        LastName = "Castro",
                        Birthdate = DateTime.Now,
                        Picture = "picture.jpg"
                    },
                    Summary = "I'm a great developer :-D",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = Guid.NewGuid(),
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
                        Direction = "16 street",
                        Email = "marcelo.ruiz@fundacion-jala.org",
                        Phone = 123456789,
                    },
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    PersonalInformation = new PersonalInformation
                    {
                        FirstName = "Marcelo",
                        LastName = "Ruiz",
                        Birthdate = DateTime.Now,
                        Picture = "picture.jpg"
                    },
                    Summary = "I'm a great ux :-D",
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            NameSkill = "Figma"
                        },
                        new Skill
                        {
                            Id = Guid.NewGuid(),
                            NameSkill = "HTML"
                        },
                    }
                }
            };
            return resumes;
        }
    }
}
