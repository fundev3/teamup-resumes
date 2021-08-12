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
        private readonly Mock<IResumesSkillsService> mockResumesSkillsService;
        private readonly Mock<ISkillsService> mockSkillsService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PostResumeSkills postResumeSkills;

        public PostResumeSkillsTests()
        {
            this.mockResumesSkillsService = new Mock<IResumesSkillsService>();
            this.mockSkillsService = new Mock<ISkillsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.postResumeSkills = new PostResumeSkills(this.mockResumesSkillsService.Object, this.mockSkillsService.Object);
        }

        [Fact]
        public void PostResume_Returns_CreateResume_Resume()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesSkillsService.Setup(service => service.AddResumeSkills(null, Guid.NewGuid())).Returns(new List<ResumeSkill>() { new ResumeSkill() }.ToArray());
            var response = this.postResumeSkills.CreateResumeSkills(request, Guid.NewGuid());
            var createdResult = Assert.IsType<CreatedResult>(response);
            Assert.IsType<ResumeSkill[]>(createdResult.Value);
        }
    }
}
