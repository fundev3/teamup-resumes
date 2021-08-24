namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
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

    public class GetPostulationsByProjectId
    {
        /*private readonly IPostulationsService postulationsService;

        public GetPostulationsByProjectId(IPostulationsService postulationsService)
        {
            this.postulationsService = postulationsService;
        }*/

        [FunctionName("GetPostulationsByProjectId")]
        [OpenApiOperation(operationId: "GetPostulationsByProjectId", tags: new[] { "Postulations" })]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The Id of the project to search by.")]

        // [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Postulations[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public bool Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/postulations")] HttpRequest req)
        {
            /*try
            {
                req.Query.TryGetValue("projectId", out StringValues projectId);
                var postulations = this.skillsService.GetSkills(projectId);
                if (postulations.Length == 0)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(postulations);
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
            }*/
            return true;
        }
    }
}
