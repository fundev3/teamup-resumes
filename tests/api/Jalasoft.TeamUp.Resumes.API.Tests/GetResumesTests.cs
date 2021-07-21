namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GetResumesTests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly Mock<HttpRequest> mockRequest;
        private readonly GetResumes getResumes;

        public GetResumesTests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockRequest = new Mock<HttpRequest>();
            this.getResumes = new GetResumes(this.mockService.Object);
        }

        [Fact]
        public void GetResumes_Returns_OkObjectResult()
        {
            Assert.IsType<OkObjectResult>(this.getResumes.Run(this.mockRequest.Object));
        }
    }
}
