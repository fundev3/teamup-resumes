namespace Jalasoft.TeamUp.Resumes.API.Controllers
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

    public class PostResume
    {
        private readonly ICreateResumeService createResumeService;

        public PostResume(ICreateResumeService createResumeService)
        {
            this.createResumeService = createResumeService;
        }

        [FunctionName("createResume")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "createResume" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var createResume = this.createResumeService.PostResume();
            return new OkObjectResult(createResume);
        }
    }
}