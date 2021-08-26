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

    public class PatchPostulationStateApiTests
    {
        private readonly Mock<IPostulationsService> mockPostulationsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PatchPostulation patchPostulation;

        public PatchPostulationStateApiTests()
        {
            this.mockPostulationsService = new Mock<IPostulationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.patchPostulation = new PatchPostulation(this.mockPostulationsService.Object);
        }

        [Fact]
        public void PatchPostulation_ExistentId_OkObjectResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Returns(new Postulation());
            var response = this.patchPostulation.Patch(request);
            var updatedResult = Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void PatchPostulation_UnexistentId_NotFound()
        {
            var request = this.mockHttpContext.Request;
            Postulation postulation = null;
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Returns(postulation);
            var response = this.patchPostulation.Patch(request);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, updatedResult.StatusCode);
        }

        [Fact]
        public void PatchPostulation_UnexpectedError_InternalError()
        {
            var request = this.mockHttpContext.Request;
            this.mockPostulationsService.Setup(service => service.PatchPostulation(It.IsAny<Postulation>())).Throws(new Exception());
            var response = this.patchPostulation.Patch(request);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, updatedResult.StatusCode);
        }
    }
}
