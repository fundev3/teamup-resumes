namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
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

        [FunctionName("PostResume")]
        [OpenApiOperation(operationId: "CreateResume", tags: new[] { "Resumes" })]
        [OpenApiRequestBody("application/json", typeof(Resume), Description = "JSON request body containing { FirstName, LastName, Email, Phone, Summary, Picture}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Resume), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Resource bad request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult CreateResume(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/resumes")] HttpRequest req)
        {
            try
            {
                Resume createResume = new Resume();
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var input = JsonConvert.DeserializeObject<Resume>(requestBody);
                createResume = this.resumesService.PostResumes(input);
                return new CreatedResult("v1/resumes/:id", createResume);
            }
            catch (ValidationException exVal)
            {
                var errorException = new ResumesException(ResumesErrors.BadRequest, exVal);
                return errorException.Error;
            }
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}