namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetResumesTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly Mock<HttpRequest> mockRequest;
        private readonly GetResumes getResumes;

        public GetResumesTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockRequest = new Mock<HttpRequest>();
            this.getResumes = new GetResumes(this.mockService.Object);
        }

        [Fact]
        public void GetResumes_Returns_OkObjectResult()
        {
            Assert.IsType<OkObjectResult>(this.getResumes.Run(this.mockRequest.Object));
        }

        [Fact]
        public void GetResumes_Returns_NotOkResult()
        {
            Assert.IsNotType<CreatedResult>(this.getResumes.Run(this.mockRequest.Object));
        }

        [Fact]
        public void GetResumes_Returns_EmptyResult()
        {
            this.mockService.Setup(service => service.GetResumes()).Returns(new Resume[0]);

            var result = this.getResumes.Run(this.mockRequest.Object);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resumes = Assert.IsType<Resume[]>(okObjectResult.Value);
            Assert.Empty(resumes);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            this.mockService.Setup(service => service.GetResumes()).Returns(this.GetTestResumes());

            var result = this.getResumes.Run(this.mockRequest.Object);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resumes = Assert.IsType<Resume[]>(okObjectResult.Value);
            Assert.Equal(2, resumes.Length);
        }

        public Resume[] GetTestResumes()
        {
            var resumes = new Resume[]
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
