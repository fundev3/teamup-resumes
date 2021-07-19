namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
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
                    FirstName = "Guido",
                    LastName = "Castro",
                    Email = "guido.castro@fundacion-jala.org",
                    Phone = "123-456-789",
                    Summary = "I'm a great developer :-D"
                },
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Marcelo",
                    LastName = "Ruiz",
                    Email = "marcelo.ruiz@fundacion-jala.org",
                    Phone = "789-456-123",
                    Summary = "I'm a great ux :-D"
                }
            };
            return resumes;
        }
    }
}
