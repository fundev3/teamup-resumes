namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PatchPostulationStateCoreTests
    {
        private readonly PostulationsService postulationService;
        private readonly Mock<IPostulationsRepository> mockPostulation;

        public PatchPostulationStateCoreTests()
        {
            this.mockPostulation = new Mock<IPostulationsRepository>();
            this.postulationService = new PostulationsService(this.mockPostulation.Object);
        }

        [Fact]
        public void PatchPostulationState_ValidId_Postulation()
        {
            var stubPostulation = new Postulation
            {
                ProjectId = "12332",
                ResumeId = 1,
                ProjectName = "TeamUp",
                ResumeName = "Jorge Lopez",
                Picture = "test.png",
                CreationDate = DateTime.Now.AddDays(-10),
                LastUpdate = DateTime.Now,
                State = "Applied"
            };

            this.mockPostulation.Setup(repository => repository.Add(stubPostulation)).Returns(new Postulation());
            var result = this.postulationService.PatchPostulation(stubPostulation);
            Assert.IsType<Postulation>(result);
        }

        [Fact]
        public void PatchPostulationState_InvalidId_Null()
        {
            var stubPostulation = new Postulation
            {
                ProjectId = "1235",
                ResumeId = 1,
                ProjectName = string.Empty,
                ResumeName = string.Empty,
                Picture = string.Empty,
                CreationDate = DateTime.Now.AddDays(-10),
                LastUpdate = DateTime.Now,
                State = "Applied"
            };

            Postulation postulation = null;
            this.mockPostulation.Setup(repository => repository.Update(It.IsAny<Postulation>())).Returns(postulation);
            var result = this.postulationService.PatchPostulation(stubPostulation);
            Assert.Null(result);
        }
    }
}
