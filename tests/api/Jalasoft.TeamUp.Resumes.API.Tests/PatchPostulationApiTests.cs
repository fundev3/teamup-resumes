namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;
    using Jalasoft.TeamUp.Projects.API.Tests.Utils;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class PatchPostulationApiTests
    {
        private readonly Mock<IPostulationsService> mockPostulationsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PatchPostulation patchPostulation;

        public PatchPostulationApiTests()
        {
            this.mockPostulationsService = new Mock<IPostulationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.patchPostulation = new PatchPostulation(this.mockPostulationsService.Object);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/state"", ""value"" : ""Accepted""},
        ]")]
        public async void PatchPostulation_ExistentId_OkObjectResult(string body)
        {
            var request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Returns(StubPostulation.GetPostulation());
            this.mockPostulationsService.Setup(service => service.GetPostulation(It.IsAny<int>())).Returns(StubPostulation.GetPostulation());
            var response = await this.patchPostulation.Patch(request, 1);
            var updatedResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, updatedResult.StatusCode);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/state"", ""value"" : ""Accepted""},
        ]")]
        public async void PatchPostulation_UnexistentId_NotFound(string body)
        {
            var request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            Postulation postulation = null;
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Returns(postulation);
            var response = await this.patchPostulation.Patch(request, 8);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, updatedResult.StatusCode);
        }

        [Theory]
        [InlineData(@"[
        {""op"" : ""replace"", ""path"" : ""/state"", ""value"" : ""BadRequest""},
        ]")]
        public async void PatchPostulation_UnexpectedError_BadRequest(string body)
        {
            var request = this.mockHttpContext.Request;
            request.Body = SetStream.Setstream(body);
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Throws(new ValidationException("Bad Request"));
            this.mockPostulationsService.Setup(service => service.GetPostulation(It.IsAny<int>())).Returns(StubPostulation.GetPostulation());
            var response = await this.patchPostulation.Patch(request, 8);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, updatedResult.StatusCode);
        }

        [Fact]
        public async void PatchPostulation_UnexpectedError_InternalError()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.GetPostulation(It.IsAny<int>())).Returns(StubPostulation.GetPostulation());
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Throws(new Exception());
            var response = await this.patchPostulation.Patch(request, 8);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, updatedResult.StatusCode);
        }
    }
}
