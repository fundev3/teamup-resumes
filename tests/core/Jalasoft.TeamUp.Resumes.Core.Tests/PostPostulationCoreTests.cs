namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Core;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class PostPostulationCoreTests
    {
        private readonly PostulationsService postulationService;
        private readonly Mock<IPostulationsRepository> mockPostulation;

        public PostPostulationCoreTests()
        {
            this.mockPostulation = new Mock<IPostulationsRepository>();
            this.postulationService = new PostulationsService(this.mockPostulation.Object);
        }

        [Fact]
        public void PostPostulation_ValidPostulation_Postulation()
        {
            var stubPostulation = new Postulation
            {
                ProjectId = Guid.NewGuid(),
                ResumeId = 1,
                ProjectName = "TeamUp",
                ResumeName = "Jorge Lopez",
                Picture = "test",
                StartDate = DateTime.Now.AddDays(-10),
                ExpireDate = DateTime.Now,
                Status = "Applied"
            };

            this.mockPostulation.Setup(repository => repository.Add(stubPostulation)).Returns(new Postulation());
            var result = this.postulationService.PostPostulation(stubPostulation);
            Assert.IsType<Postulation>(result);
        }

        [Fact]
        public void PostPostulation_InvalidPostulation_ValidationException()
        {
            var stubPostulation = new Postulation
            {
                ProjectId = Guid.NewGuid(),
                ResumeId = 1,
                ProjectName = string.Empty,
                ResumeName = string.Empty,
                Picture = string.Empty,
                StartDate = DateTime.Now.AddDays(-10),
                ExpireDate = DateTime.Now,
                Status = "Applied"
            };

            this.mockPostulation.Setup(repository => repository.Add(stubPostulation)).Returns(new Postulation());

            Assert.Throws<FluentValidation.ValidationException>(() => this.postulationService.PostPostulation(stubPostulation));
        }
    }
}
