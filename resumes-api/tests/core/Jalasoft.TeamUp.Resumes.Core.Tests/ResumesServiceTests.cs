namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class ResumesServiceTests
    {
        private readonly Mock<IResumesRepository> mockRepository;
        private readonly ResumesService resumesService;

        public ResumesServiceTests()
        {
            this.mockRepository = new Mock<IResumesRepository>();
            this.resumesService = new ResumesService(this.mockRepository.Object);
        }

        [Fact]
        public void GetResumes_Returns_EmptyResult()
        {
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(new List<Resume>());

            var result = this.resumesService.GetResumes();

            Assert.Empty(result);
        }

        [Fact]
        public void GetResumes_Returns_AllItemsResult()
        {
            this.mockRepository.Setup(repository => repository.GetResumes()).Returns(this.GetTestResumes());

            var result = this.resumesService.GetResumes();

            Assert.Equal(2, result.Length);
        }

        public IEnumerable<Resume> GetTestResumes()
        {
            var resumes = new List<Resume>
            {
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Guido",
                    LastName = "Castro",
                    Email = "guido.castro@fundacion-jala.org",
                    Phone = "123-456-789",
                    Summary = "I'm a great developer :-D"
                },
                new Resume()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Marcelo",
                    LastName = "Ruiz",
                    Email = "marcelo.ruiz@fundacion-jala.org",
                    Phone = "789-456-123",
                    Summary = "I'm a great ux :-D"
                }
            };
            return resumes;
        }
    }
}
