namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class GetSkillsByName
    {
        private readonly ISkillsService skillsService;

        public GetSkillsByName(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [FunctionName("GetSkillsByName")]
        [OpenApiOperation(operationId: "GetSkillsByName", tags: new[] { "Skills" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Skill[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]

        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/skills/{name}")] HttpRequest req, string name)
        {
            Console.WriteLine("esta en skillservice.getskillbyname");
            var result = this.skillsService.GetSkillByName(name);

            if (result == null)
            {
                return new NotFoundObjectResult(result);
            }
            else
            {
                return new OkObjectResult(result);
            }
        }
    }
}
