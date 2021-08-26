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
                    Id = 1,
                    ProjectId = "7ca39055-b22a-4826-9304-318009778f6b",
                    ResumeId = 1,
                    ProjectName = "JalaTalk",
                    ResumeName = "Lina",
                    Picture = "test.png",
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    State = "postulated"
                },
                new Postulation()
                {
                    Id = 2,
                    ProjectId = "7ca39055-b22a-4826-9304-318009778f6b",
                    ResumeId = 2,
                    ProjectName = "JalaTalk",
                    ResumeName = "Paulo",
                    Picture = "test.png",
                    CreationDate = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    State = "postulated"
                }
            };
            return postulations;
        }

        [Fact]
        public void GetPostulationsByResumeId_ValidResumeId_Postulations()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetPostulationsByResumeId(It.IsAny<int>())).Returns(GetTestPostulations);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<int>());

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetPostulationsByResumeId_ValidResumeId_EmptyPostulations()
        {
            // Arrange
            var stubEmptyProjectList = new List<Postulation>();
            this.mockRepository.Setup(repository => repository.GetPostulationsByResumeId(It.IsAny<int>())).Returns(stubEmptyProjectList);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<int>());

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetPostulationsByProjectId_ValidProjectId_Postulations()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAllByProjectId(It.IsAny<string>())).Returns(GetTestPostulations());

            // Act
            var result = this.postulationService.GetPostulationsByProjectId(It.IsAny<string>());

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetPostulationsByProjectId_ValidProjectId_EmptyPostulations()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetAllByProjectId(It.IsAny<string>())).Returns(new List<Postulation>());

            // Act
            var result = this.postulationService.GetPostulationsByProjectId(It.IsAny<string>());

            // Assert
            Assert.Empty(result);
        }
    }
}