namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;

    public class GetResumeByIdTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetResumeById getResume;

        public GetResumeByIdTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getResume = new GetResumeById(this.mockService.Object);
        }

        [Fact]
        public void GetResume_ValidId_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetResume(1)).Returns(new Resume());

            // Act
            var response = this.getResume.Run(request, 1);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume>(okObjectResult.Value);
        }

        [Fact]
        public void GetResume_InvalidResume_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetResume(2)).Throws(new ResumeException(ErrorsTypes.NotFoundError, new Exception()));

            // Act
            var response = this.getResume.Run(request, 2);

            // Assert
            var notFoundObjectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }
    }
}
