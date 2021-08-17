namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
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

    public class PutResume
    {
        private readonly IResumesService resumesService;

        public PutResume(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("UpdateResume")]
        [OpenApiOperation(operationId: "CreateResumeSkills", tags: new[] { "ResumeSkill" })]
        [OpenApiRequestBody("application/json", typeof(List<Skill>), Description = "JSON request body containing { Id, Name }")]
        [OpenApiParameter(name: "idResume", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        public IActionResult UpdateResume(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/resumes/{idResume}/skills")] HttpRequest req, int idResume)
        {
            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var skills = JsonConvert.DeserializeObject<List<Skill>>(requestBody);

                var updateResume = this.resumesService.UpdateResume(new Resume() { Id = idResume, Skills = skills?.ToArray() });
                return new OkObjectResult(updateResume?.Skills);
            }
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
