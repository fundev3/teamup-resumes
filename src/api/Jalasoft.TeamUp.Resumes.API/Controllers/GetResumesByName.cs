﻿namespace Jalasoft.TeamUp.Resumes.API.Controllers
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

    public class GetResumesByName
    {
        private readonly IResumesService resumesService;

        public GetResumesByName(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("GetResumesByName")]
        [OpenApiOperation(operationId: "GetResumesByName", tags: new[] { "Resumes" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The name of the skill to search by.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes-by-name")] HttpRequest req)
        {
            try
            {
                req.Query.TryGetValue("name", out StringValues name);
                var resumes = this.resumesService.GetByName(name);
                if (resumes.Length == 0)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(resumes);
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
