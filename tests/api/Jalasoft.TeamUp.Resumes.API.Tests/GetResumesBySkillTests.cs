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

    public class GetResumesBySkillTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetResumesBySkill getResumesBySkill;

        public GetResumesBySkillTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getResumesBySkill = new GetResumesBySkill(this.mockService.Object);
        }

        [Fact]
        public void GetResumes_ValidSkill_Resume()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetBySkill(null)).Returns(new Resume[2]);

            // Act
            var response = this.getResumesBySkill.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetResumes_InvalidResponse_Error500()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            var res = this.mockService.Setup(service => service.GetBySkill(null)).Throws(new ResumesException(ResumesErrors.InternalServerError, new Exception()));

            // Act
            var response = this.getResumesBySkill.Run(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public void GetSkillsByName_InvalidSkill_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetBySkill(null)).Throws(new ResumesException(ResumesErrors.NotFound));

            // Act
            var response = this.getResumesBySkill.Run(request);

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}
