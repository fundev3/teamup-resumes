namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class PostPostulationApiTests
    {
        private readonly Mock<IPostulationsService> mockPostulationsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostPostulation postPostulation;

        public PostPostulationApiTests()
        {
            this.mockPostulationsService = new Mock<IPostulationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postPostulation = new PostPostulation(this.mockPostulationsService.Object);
        }

        [Fact]
        public void PostPostulation_ValidPostulation_Created()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.PostPostulation(It.IsAny<Postulation>())).Returns(new Postulation());
            var response = this.postPostulation.Run(request);
            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.IsType<Postulation>(createdResult.Value);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact]
        public void PostPostulation_InvalidPostulation_BadRequest()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.PostPostulation(null)).Throws(new ValidationException("BadRequest"));
            var response = this.postPostulation.Run(request);
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void PostPostulation_UnexpectedError_InternalError()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.PostPostulation(It.IsAny<Postulation>())).Throws(new Exception());
            var response = this.postPostulation.Run(request);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, updatedResult.StatusCode);
        }
    }
}
