namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
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

        public static Postulation[] GetTestPostulations()
        {
            var postulations = new List<Postulation>
            {
                new Postulation()
                {
                    Id = 1,
                    ProjectId = "7ca39055-b22a-4826-9304-318009778f6b",
                    ResumeId = 1,
                    ProjectName = "JalaTalk",
                    ResumeName = "Lina",
                    Picture = "test.png",
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    State = "postulated"
                },
                new Postulation()
                {
                    Id = 2,
                    ProjectId = "7ca39055-b22a-4826-9304-318009778f6b",
                    ResumeId = 2,
                    ProjectName = "JalaTalk",
                    ResumeName = "Paulo",
                    Picture = "test.png",
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    State = "postulated"
                }
            };
            return postulations.ToArray();
        }

        [Fact]
        public void GetPostulationsByProjectId_ValidProjectId_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetPostulationsByProjectId(It.IsAny<string>())).Returns(GetTestPostulations());

            // Act
            var response = this.getPostulationsByProjectId.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.IsType<Postulation[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetPostulationsByProjectId_UnexpectedError_InternalError()
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

        [Fact]
        public void GetPostulationsByProjectId_InvalidProjectId_BadRequest()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetPostulationsByProjectId(It.IsAny<string>())).Returns(new Postulation[0]);

            // Act
            var response = this.getPostulationsByProjectId.Run(request);

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}