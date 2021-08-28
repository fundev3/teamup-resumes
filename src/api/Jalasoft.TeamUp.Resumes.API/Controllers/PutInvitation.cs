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
        [OpenApiRequestBody("application/json", typeof(Skill[]), Description = "JSON request body containing list of skills")]
        [OpenApiParameter(name: "invitationId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Skill[]), Description = "Successful response")]
        public IActionResult UpdateResumeSkill(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/resumes/invitations/{invitationId}&{gatito}")] HttpRequest req, Guid invitationId)
        {
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var invitation = JsonConvert.DeserializeObject(requestBody);
                var result = this.invitationsService.UpdateInvitation(invitationId);

                // var response = this.invitationsService.UpdateProject(idresume, projectId);
                return new OkObjectResult(result);
            }
        }
    }
}
