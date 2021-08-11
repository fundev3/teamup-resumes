namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class PostResumesApiTests
    {
        private readonly Mock<IResumesService> mockResumesService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostResume postResume;

        public PostResumesApiTests()
        {
            this.mockResumesService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postResume = new PostResume(this.mockResumesService.Object);
        }

        [Fact]
        public void PostResume_Returns_CreateResume_Resume()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesService.Setup(service => service.PostResumes(null)).Returns(new Resume());
            var response = this.postResume.CreateResume(request);
            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.IsType<Resume>(createdResult.Value);
        }

        [Fact]
        public void PostResume_Returns_BadRequest()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesService.Setup(service => service.PostResumes(null)).Throws(new ResumesException(ResumesErrors.BadRequest, new FluentValidation.ValidationException("BadRequest")));
            var response = this.postResume.CreateResume(request);
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
