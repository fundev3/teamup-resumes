namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System.IO;
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PostResume
    {
        private readonly IResumesService resumesService;

        public PostResume(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("resume")]
        [OpenApiOperation(operationId: "createResume", tags: new[] { "Resumes" })]
        [OpenApiRequestBody("application/json", typeof(Resume), Description = "JSON request body containing { FirstName, LastName, Email, Phone, Summary}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        public IActionResult CreateResume(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "resumes")] HttpRequest req)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            var input = JsonConvert.DeserializeObject<Resume>(requestBody);
            var createResume = this.resumesService.PostResumes(input);
            return new OkObjectResult(createResume);
        }
    }
}