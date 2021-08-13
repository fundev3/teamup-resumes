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

    public class PostResumeSkillsTests
    {
        private readonly Mock<IResumesService> mockResumesService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostResumeSkills postResumeSkills;

        public PostResumeSkillsTests()
        {
            this.mockResumesService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postResumeSkills = new PostResumeSkills(this.mockResumesService.Object);
        }

        [Fact]
        public void PostResume_Returns_CreateResult_Resume()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesService.Setup(service => service.UpdateResume(new Resume())).Returns(new Resume());
            var response = this.postResumeSkills.CreateResumeSkills(request, Guid.NewGuid());
            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.IsType<Resume>(createdResult.Value);
        }
    }
}
