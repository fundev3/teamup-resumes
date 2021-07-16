namespace Jalasoft.TeamUp.Resumes.API
{
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
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

        [FunctionName("health")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Health" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Health), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var healths = this.healthService.GetHealth();

            return new OkObjectResult(healths);
        }
    }
}