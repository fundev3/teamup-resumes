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

    public class GetSkillsByNameTests
    {
        private readonly Mock<ISkillsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetSkillsByName getSkillsByName;

        public GetSkillsByNameTests()
        {
            this.mockService = new Mock<ISkillsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getSkillsByName = new GetSkillsByName(this.mockService.Object);
        }

        [Fact]
        public void GetSkillByName_ExistName_SkillsArray()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetSkills(".NET")).Returns(new Skill[2]);

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Skill[]>(okObjectResult.Value);
        }
    }
}
