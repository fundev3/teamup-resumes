namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System.Net;
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

    public class GetSkillsByName
    {
        private readonly ISkillsService skillsService;

        public GetSkillsByName(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [FunctionName("GetSkillsByName")]
        [OpenApiOperation(operationId: "GetSkillsByName", tags: new[] { "Skills" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The name of the skill to search by.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Skill[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/skills")] HttpRequest req)
        {
            try
            {
                req.Query.TryGetValue("name", out StringValues name);
                var skills = this.skillsService.GetSkills(name);
                if (skills.Length == 0)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(skills);
            }
            catch (ResumesException ex)
            {
                var error = ex.Error;
                return error;
            }
            catch (System.Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
