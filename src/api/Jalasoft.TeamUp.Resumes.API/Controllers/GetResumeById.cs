﻿namespace Jalasoft.TeamUp.Resumes.API.Controllers
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

    public class GetResumeById
    {
        private readonly IResumesService resumesService;

        public GetResumeById(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("GetResumeById")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Resumes" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes/{id:guid}")] HttpRequest req, Guid id)
        {
            var result = this.resumesService.GetResume(id);

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
