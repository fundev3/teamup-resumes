namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class UpdateResumeTests
    {
        private readonly Mock<IResumesService> mockResumesService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PutResume postResumeSkills;

        public UpdateResumeTests()
        {
            this.mockResumesService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postResumeSkills = new PutResume(this.mockResumesService.Object);
        }

        [Fact]
        public void UpdateResume_Returns_OkObjectResult()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesService.Setup(service => service.UpdateResume(It.IsAny<Resume>())).Returns(new Resume());
            var response = this.postResumeSkills.UpdateResume(request, Guid.Parse("FD5BB199-3ED6-4519-BBCE-5FA7A2C40329"));
            var createdResult = Assert.IsType<OkObjectResult>(response);
        }
    }
}
