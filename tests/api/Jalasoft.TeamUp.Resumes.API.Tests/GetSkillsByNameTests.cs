namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetSkillsByNameTests
    {
        private readonly Mock<ISkillsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetSkillsByName getSkillsByName;
        private readonly SkillsApiRepository skillsApiRepository;

        public GetSkillsByNameTests()
        {
            this.mockService = new Mock<ISkillsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getSkillsByName = new GetSkillsByName(this.mockService.Object);
            this.skillsApiRepository = new SkillsApiRepository();
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

        [Fact]
        public void GetSkillByName_Returns_Skills()
        {
            // Arrange
            var emsiSkills = this.skillsApiRepository.GetSkills("TypeScript");
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetSkills("TypeScript")).Returns(emsiSkills.ToArray());

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Skill[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetSkillByName_Validate_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetSkills("Julio")).Throws(new ResumeException(ErrorsTypes.NotFoundError, new Exception()));

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var notFoundObjectResult = Assert.IsType<ObjectResult>(response);
            Assert.NotNull(notFoundObjectResult.StatusCode);
        }
    }
}