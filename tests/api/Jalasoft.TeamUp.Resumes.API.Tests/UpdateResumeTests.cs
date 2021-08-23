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

    public class UpdateResumeTests
    {
        private readonly Mock<IResumesService> mockResumesService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly PutResumeSkill putResume;

        public UpdateResumeTests()
        {
            this.mockResumesService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.putResume = new PutResumeSkill(this.mockResumesService.Object);
        }

        [Fact]
        public void UpdateResume_ExistentId_OkObjectResult()
        {
            var request = this.mockHttpContext.Request;
            IEnumerable<Skill> skills = new List<Skill>() { new Skill() };
            this.mockResumesService.Setup(service => service.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(skills);
            var response = this.putResume.UpdateResumeSkill(request, 1);
            var updatedResult = Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void UpdateResume_UnexistentId_NotFound()
        {
            var request = this.mockHttpContext.Request;
            IEnumerable<Skill> skills = new List<Skill>();
            this.mockResumesService.Setup(service => service.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Returns(skills);
            var response = this.putResume.UpdateResumeSkill(request, 7);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, updatedResult.StatusCode);
        }

        [Fact]
        public void UpdateResume_UnexpectedError_InternalError()
        {
            var request = this.mockHttpContext.Request;
            this.mockResumesService.Setup(service => service.UpdateResumeSkill(It.IsAny<int>(), It.IsAny<Skill[]>())).Throws(new Exception());
            var response = this.putResume.UpdateResumeSkill(request, 1);
            var updatedResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, updatedResult.StatusCode);
        }
    }
}
