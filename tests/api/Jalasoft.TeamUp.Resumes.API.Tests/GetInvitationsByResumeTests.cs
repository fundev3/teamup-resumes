namespace Jalasoft.TeamUp.Resumes.API.Tests
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.API.Controllers;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Moq;
    using Xunit;

    public class GetInvitationsByResumeTests
    {
        private readonly Mock<IInvitationsService> mockService;
        private readonly DefaultHttpContext mockHttpContext;
        private readonly GetInvitationsByResume getInvitation;

        public GetInvitationsByResumeTests()
        {
            this.mockService = new Mock<IInvitationsService>();
            this.mockHttpContext = new DefaultHttpContext();
            this.getInvitation = new GetInvitationsByResume(this.mockService.Object);
        }

        [Fact]
        public void GetInvitationsByResume_Returns_ThrowException404()
        {
            // Arrange
            var request = this.mockHttpContext.Request;
            this.mockService.Setup(service => service.GetInvitations(Guid.Parse("5a7939fd-59de-44bd-a092-f5d8434584de"))).Throws(new ResumeException(ErrorsTypes.NotFoundError, new Exception()));

            // Act
            var response = this.getInvitation.Run(request, new Guid("5a7939fd-59de-44bd-a092-f5d8434584de"));

            // Assert
            var notFoundObjectResult = Assert.IsType<ObjectResult>(response);
            Assert.NotNull(notFoundObjectResult.StatusCode);
        }
    }
}
