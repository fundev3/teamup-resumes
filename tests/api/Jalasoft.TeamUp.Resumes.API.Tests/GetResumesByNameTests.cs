namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetResumesByNameTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetResumesByName getResumesByName;

        public GetResumesByNameTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getResumesByName = new GetResumesByName(this.mockService.Object);
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
        public void GetResumeByName_ValidId_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetByName("Gustavo")).Returns(GetTestResumes().ToArray());

            // Act
            var response = this.getResumesByName.Run(request, "Gustavo");

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetResumesByName_InvalidResume_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetByName("Julio")).Throws(new ResumesException(ResumesErrors.NotFound));

            // Act
            var response = this.getResumesByName.Run(request, "Julio");

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}