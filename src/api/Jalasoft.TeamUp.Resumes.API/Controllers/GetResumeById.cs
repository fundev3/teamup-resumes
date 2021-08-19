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
    using Microsoft.OpenApi.Models;

    public class GetResumeById
    {
        private readonly IResumesService resumesService;

        public GetResumeById(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("GetResumeById")]
        [OpenApiOperation(operationId: "GetResumeById", tags: new[] { "Resumes" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes/{id:int}")] HttpRequest req, int id)
        {
            try
            {
                var result = new Resume();
                result = this.resumesService.GetResume(id);
                if (result == null)
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
