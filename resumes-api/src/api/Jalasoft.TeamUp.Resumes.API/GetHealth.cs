namespace Jalasoft.TeamUp.Resumes.API
{
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;

    public static class GetHealth
    {
        [FunctionName("GetHealth")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "GetHealth" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            string responseMessage = "I'm resumes-api and I'm alive and running! ;)";

            return new OkObjectResult(responseMessage);
        }
    }
}