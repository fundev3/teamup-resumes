namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Primitives;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PutInvitation
    {
        private readonly IInvitationsService invitationsService;

        public PutInvitation(IInvitationsService invitationsService)
        {
            this.invitationsService = invitationsService;
        }

        [FunctionName("UpdateInvitation")]
        [OpenApiOperation(operationId: "UpdateInvitation", tags: new[] { "ResumeSkills" })]
        [OpenApiRequestBody("application/json", typeof(Invitation), Description = "JSON request body containing list of Invitations")]
        [OpenApiParameter(name: "invitationId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The Invitation identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Invitation), Description = "Successful response")]
        public IActionResult UpdateResumeSkill(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/invitations/{invitationId}")] HttpRequest req, Guid invitationId)
        {
            {
                try
                {
                    string requestBody = new StreamReader(req.Body).ReadToEnd();
                    var invitations = JsonConvert.DeserializeObject(requestBody);
                    var result = this.invitationsService.UpdateInvitation(invitationId);
                    req.Query.TryGetValue("projectId", out StringValues projectId);
                    req.Query.TryGetValue("projectId", out StringValues idResume);
                    var response = this.invitationsService.UpdateProject(int.Parse(idResume), Guid.Parse(projectId));
                    return new OkObjectResult(result);
                }
                catch (ResumesException e)
                {
                    return e.Error;
                }
                catch (System.Exception e)
                {
                    var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                    return errorException.Error;
                }
            }
        }
    }
}
