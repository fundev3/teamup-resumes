namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetSkillsTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetSkills getSkills;

        public GetSkillsTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getSkills = new GetSkills(this.mockService.Object);
        }

        [Fact]
        public void GetSkills_Returns_OkObjectResult()
        {
            // Arrange
            var request = this.mockHttpContext.Request;

            // Act
            var response = this.getSkills.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Resume[]>(okObjectResult.Value);
        }
    }
}