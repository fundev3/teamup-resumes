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

    public class PutResumeSkill
    {
        private readonly IResumesService resumesService;

        public PutResumeSkill(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("UpdateResumeSkill")]
        [OpenApiOperation(operationId: "UpdateResumeSkill", tags: new[] { "ResumeSkills" })]
        [OpenApiRequestBody("application/json", typeof(List<Skill>), Description = "JSON request body containing list of skills")]
        [OpenApiParameter(name: "idResume", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        public IActionResult UpdateResume(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/resumes/{idResume}/skills")] HttpRequest req, int idResume)
        {
            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var skills = JsonConvert.DeserializeObject<Skill[]>(requestBody);

                var updateResume = this.resumesService.UpdateResumeSkill(idResume, skills);

                if (updateResume == null)
                {
                    return new ResumesException(ResumesErrors.NotFound).Error;
                }

                return new OkObjectResult(updateResume);
            }
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
