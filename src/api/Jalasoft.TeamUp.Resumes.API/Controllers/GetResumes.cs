namespace Jalasoft.TeamUp.Resumes.API.Controllers
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
                if (resumes == null)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(resumes);
            }
            catch (ResumesException e)
            {
                return e.Error;
            }
            catch (System.Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}