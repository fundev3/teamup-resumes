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
                    ProjectId = Guid.NewGuid(),
                    ResumeId = 1,
                    ProjectName = "TeamUp",
                    ResumeName = "Jorge Lopez",
                    Picture = "test",
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now,
                    State = "Applied"
                },
                new Postulation()
                {
                    ProjectId = Guid.NewGuid(),
                    ResumeId = 2,
                    ProjectName = "CoreProject",
                    ResumeName = "Alex Flores",
                    Picture = "test",
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now,
                    State = "Applied"
                }
            };
            return postulations;
        }

        [Fact]
        public void GetResumes_NoItems_EmptyResult()
        {
            // Arrange
            var stubEmptyPostulationList = new List<Postulation>();
            this.mockRepository.Setup(repository => repository.GetPostulationsById(It.IsAny<string>())).Returns(stubEmptyPostulationList);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetPostulationsById(null)).Returns(GetTestPostulations().ToList());

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void GetResume_ValidId_SingleResume()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetPostulationsById(It.IsAny<string>())).Returns(GetTestPostulations);

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetResumesBySkill_ItemsExist_ResumesArray()
        {
            // Arrange
            this.mockRepository.Setup(repository => repository.GetPostulationsById(It.IsAny<string>())).Returns(GetTestPostulations().ToList());

            // Act
            var result = this.postulationService.GetPostulations(It.IsAny<string>());

            // Assert
            Assert.Equal(2, result.Length);
        }
    }
}