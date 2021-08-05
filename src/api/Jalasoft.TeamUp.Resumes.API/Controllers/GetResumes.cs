namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;
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
        [OpenApiOperation(operationId: "GetResumes", tags: new[] { "Resumes" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume[]), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes")] HttpRequest req)
        {
            try
            {
                var resumes = this.resumesService.GetResumes();
                return new OkObjectResult(resumes);
            }
            catch (ResumeException ex)
            {
                var error = new ObjectResult(ex.Error.ErrorMessage);
                error.StatusCode = ex.Error.Code;
                return error;
            }
        }
    }
}