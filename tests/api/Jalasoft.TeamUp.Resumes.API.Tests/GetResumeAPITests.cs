namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;

    public class GetResumeAPITests
    {
        private readonly Mock<IResumesService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetResumes getResume;

        public GetResumeAPITests()
        {
            this.mockService = new Mock<IResumesService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getResume = new GetResumes(this.mockService.Object);
        }

        [Fact]
        public void GetResume_Returns_OkObjectResult()
        {
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsType<OkObjectResult>(this.getResume.Run(request));
        }

        [Fact]
        public void GetResume_Returns_NoOkResult()
        {
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            Assert.IsNotType<CreatedResult>(this.getResume.Run(request));
        }

        [Fact]
        public void GetResume_Returns_Resume()
        {
            this.mockService.Setup(service => service.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Returns(new Resume());
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584de"));
            var result = this.getResume.Run(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resume = Assert.IsType<Resume>(okObjectResult.Value);
            Assert.NotNull(resume);
        }

        [Fact]
        public void GetResume_Returns_Null()
        {
            this.mockService.Setup(service => service.GetResume(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Equals(null);
            var request = this.mockHttpContext.Request;
            request.Query = new QueryCollection(CreateDictionary("id", "5a7939fd-59de-44bd-a092-f5d8434584dd"));
            var result = this.getResume.Run(request);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okObjectResult.Value);
        }

        private static Dictionary<string, StringValues> CreateDictionary(string key, string value)
        {
            var qs = new Dictionary<string, StringValues>
            {
                { key, value }
            };
            return qs;
        }
    }
}
