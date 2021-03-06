namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Web;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetResumesTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetResumes getResumes;

        public GetResumesTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getResumes = new GetResumes(this.mockService.Object);
        }

        [Fact]
        public void GetResumes_ItemsExists_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;

            // Act
            var response = this.getResumes.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetResumes_WithQueryParam_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetByName("Gustavo")).Returns(new Resume[10]);

            // Act
            var response = this.getResumes.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetResumes_ValidSkill_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetResumes(It.IsAny<string>())).Returns(new Resume[2]);

            // Act
            var response = this.getResumes.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetResumes_InvalidResponse_InternalError()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            var res = this.mockService.Setup(service => service.GetResumes(It.IsAny<string>())).Throws(new ResumesException(ResumesErrors.InternalServerError, new Exception()));

            // Act
            var response = this.getResumes.Run(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public void GetSkillsByName_InvalidSkill_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetResumes(It.IsAny<string>())).Throws(new ResumesException(ResumesErrors.NotFound));

            // Act
            var response = this.getResumes.Run(request);

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}