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
        private readonly IPostulationsService postulationService;

        public GetPostulations(IPostulationsService postulationService)
        {
            this.postulationService = postulationService;
        }

        [FunctionName("GetPostulationsByResumeId")]
        [OpenApiOperation(operationId: "GetPostulationsByResumeId", tags: new[] { "Postulations" })]
        [OpenApiParameter(name: "resumeId", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Postulation[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/postulation")] HttpRequest req)
        {
            try
            {
                string resumeId = req.Query["resumeId"];
                Postulation[] result = null;
                result = this.postulationService.GetPostulations(resumeId);
                if (result.Length == 0)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(result);
            }
            catch (ResumesException e)
            {
                return e.Error;
            }
            catch (Exception ex)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, ex);
                return errorException.Error;
            }
        }
    }
}
