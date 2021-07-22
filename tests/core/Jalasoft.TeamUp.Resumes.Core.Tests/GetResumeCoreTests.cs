namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class GetResumeCoreTests
    {
        private readonly Mock<IResumesRepository> mockRepository;
        private readonly ResumesService service;

        public GetResumeCoreTests()
        {
            this.mockRepository = new Mock<IResumesRepository>();
            this.service = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void GetResume_Returns_Resume()
        {
            this.mockRepository.Setup(repository => repository.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Resume());
            var result = this.service.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.NotNull(result);
        }

        [Fact]
        public void GetResume_Returns_Null()
        {
            this.mockRepository.Setup(repository => repository.GetResume(Guid.Parse("4a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);
            var result = this.service.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.Null(result);
        }
    }
}
