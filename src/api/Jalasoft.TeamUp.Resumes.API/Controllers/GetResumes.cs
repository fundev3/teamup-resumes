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

    public class GetResumes
    {
        private readonly IResumesService resumesService;

        public GetResumes(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("GetResumes")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Resumes" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume[]), Description = "Successful response")]
        public IActionResult Run(

            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes/{id}")] HttpRequest req)

            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes")] HttpRequest req)
          
        {
            Guid id = Guid.Parse(req.Query["id"]);
            var result = this.resumesService.GetResume(id);
            return new OkObjectResult(result);
        }
    }
}