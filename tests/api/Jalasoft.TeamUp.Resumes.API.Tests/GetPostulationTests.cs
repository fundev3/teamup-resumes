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

    public class GetPostulationTests
    {
        private readonly Mock<IPostulationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetPostulations getPostulations;

        public GetPostulationTests()
        {
            this.mockService = new Mock<IPostulationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getPostulations = new GetPostulations(this.mockService.Object);
        }

        [Fact]
        public void GetPostulationsByResumeId_ValidProjectId_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetPostulations(It.IsAny<string>())).Returns(new Postulation[10]);

            // Act
            var response = this.getPostulations.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Postulation[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetPostulationsByResumeId_UnexpectedError_InternalError()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            var res = this.mockService.Setup(service => service.GetPostulations(It.IsAny<string>())).Throws(new ResumesException(ResumesErrors.InternalServerError, new Exception()));

            // Act
            var response = this.getPostulations.Run(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public void GetPostulationsByResumeId_InvalidProjectId_BadRequest()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetPostulations(It.IsAny<string>())).Throws(new ResumesException(ResumesErrors.NotFound));

            // Act
            var response = this.getPostulations.Run(request);

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}
