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

    public class GetPostulations
    {
        private readonly IPostulationsService postulationsService;

        public GetPostulations(IPostulationsService postulationsService)
        {
            this.postulationsService = postulationsService;
        }

        [FunctionName("GetPostulations")]
        [OpenApiOperation(operationId: "GetPostulations", tags: new[] { "Postulations" })]
        [OpenApiParameter(name: "resumeId", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "The resume identifier.")]
        [OpenApiParameter(name: "projectId", In = ParameterLocation.Query, Required = false, Type = typeof(Guid), Description = "The Id of the project to search by.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Postulation[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/postulations")] HttpRequest req)
        {
            try
            {
                req.Query.TryGetValue("resumeId", out StringValues resumeId);
                req.Query.TryGetValue("projectId", out StringValues projectId);
                int? resumeIdNumber;
                resumeIdNumber = string.IsNullOrEmpty(resumeId) ? resumeIdNumber = null : resumeIdNumber = int.Parse(resumeId);
                Postulation[] result = null;
                if (string.IsNullOrEmpty(projectId))
                {
                    result = this.postulationsService.GetPostulations(resumeIdNumber);
                }
                else
                {
                    result = this.postulationsService.GetPostulationsByProjectId(projectId);
                }

                if (result.Length > 0 && result[0].Id == 0)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(result);
            }
            catch (ResumesException e)
            {
                return e.Error;
            }
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
