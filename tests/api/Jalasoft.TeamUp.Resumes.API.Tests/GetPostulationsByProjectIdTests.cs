namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetPostulationsByProjectIdTests
    {
        private readonly Mock<IPostulationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetPostulationsByProjectId getPostulationsByProjectId;

        public GetPostulationsByProjectIdTests()
        {
            this.mockService = new Mock<IPostulationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getPostulationsByProjectId = new GetPostulationsByProjectId(this.mockService.Object);
        }

        [Fact]
        public void GetPostulationsByProjectId_Returns_Postulations()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetPostulationsByProjectId("1")).Returns(new Postulation[2]);

            // Act
            var response = this.getPostulationsByProjectId.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Postulation[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetPostulationsByProjectId_InvalidResponse_Error500()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            var res = this.mockService.Setup(service => service.GetPostulationsByProjectId(null)).Throws(new ResumesException(ResumesErrors.InternalServerError, new Exception()));

            // Act
            var response = this.getPostulationsByProjectId.Run(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}