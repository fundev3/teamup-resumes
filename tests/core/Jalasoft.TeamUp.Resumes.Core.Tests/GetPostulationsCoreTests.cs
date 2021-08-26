namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Moq;
    using Xunit;

    public class GetPostulationsCoreTests
    {
        private readonly Mock<IPostulationsRepository> mockRepository;
        private readonly PostulationsService postulationService;

        public GetPostulationsCoreTests()
        {
            this.mockRepository = new Mock<IPostulationsRepository>();
            this.postulationService = new PostulationsService(this.mockRepository.Object);
        }

        public static IEnumerable<Postulation> GetTestPostulations()
        {
            var postulations = new List<Postulation>
            {
                new Postulation()
                {
                    ProjectId = "1231231273687812",
                    ResumeId = 1,
                    ProjectName = "TeamUp",
                    ResumeName = "Jorge Lopez",
                    Picture = "test",
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now,
                    State = "Applied"
                }
            };
            return postulations;
        }

        [Fact]
        public void GetPostulationsByResumeId_ValidResumeId_Postulations()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetPostulationsByResumeId(It.IsAny<string>())).Returns(GetTestPostulations);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPostulationsByResumeId_InvalidResumeId_ValidationException()
        {
            // Arrange
            var stubEmptyProjectList = new List<Postulation>();
            this.mockRepository.Setup(repository => repository.GetPostulationsByResumeId(It.IsAny<string>())).Returns(stubEmptyProjectList);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.Empty(result);
        }
    }
}