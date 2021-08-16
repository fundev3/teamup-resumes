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
        public void GetSkillByName_Returns_Skills()
        {
            // Arrange
            var emsiSkills = this.skillsApiRepository.GetSkills("Typescript");
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetSkills(null)).Returns(emsiSkills.ToArray());

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response);
            Assert.IsType<Skill[]>(okObjectResult.Value);
        }

        [Fact]
        public void GetSkillsByName_InvalidResponse_Error500()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            var res = this.mockService.Setup(service => service.GetSkills(null)).Throws(new ResumesException(ResumesErrors.InternalServerError, new Exception()));

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public void GetSkillsByName_InvalidSkill_NotFound()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetSkills("Julio")).Throws(new ResumesException(ResumesErrors.NotFound));

            // Act
            var response = this.getSkillsByName.Run(request);

            // Assert
            var notFound = Assert.IsType<ObjectResult>(response);
            Assert.Equal(404, notFound.StatusCode);
        }
    }
}