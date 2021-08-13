namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class GetInvitationsByResume
    {
        private readonly IInvitationsService invitationsService;

        public GetInvitationsByResume(IInvitationsService invitationsService)
        {
            this.invitationsService = invitationsService;
        }

        [FunctionName("GetInvitationsByResume")]
        [OpenApiOperation(operationId: "GetInvitationsByResume", tags: new[] { "Invitations" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Invitation), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes/{id:guid}/invitations")] HttpRequest req, Guid id)
        {
            Invitation[] result;
            try
            {
                result = this.invitationsService.GetInvitations(id);
                return new OkObjectResult(result);
            }
            catch (ResumeException ex)
            {
                var error = new ObjectResult(ex.Error.ErrorMessage);
                error.StatusCode = ex.Error.Code;
                return error;
            }
        }
    }
}
