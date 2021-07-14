namespace Jalasoft.TeamUp.Resumes.API
{
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public class GetHealth
    {
        private readonly IHealthService healthService;

        public GetHealth(IHealthService healthService)
        {
            this.healthService = healthService;
        }

        [FunctionName("GetHealth")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GetHealth" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var healths = this.healthService.GetHealth();

            return new OkObjectResult(healths);
        }
    }
}