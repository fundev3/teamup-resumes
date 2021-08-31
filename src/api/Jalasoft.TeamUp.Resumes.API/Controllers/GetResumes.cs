namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Net;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Extensions.Primitives;
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
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The name of the resume to search by.")]
        [OpenApiParameter(name: "skill", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The name of the skill to search by.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Resume[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/resumes")] HttpRequest req)
        {
            try
            {
                string skill = req.Query["skill"];
                req.Query.TryGetValue("name", out StringValues name);
                Resume[] resumes = null;
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(skill))
                {
                    resumes = this.resumesService.GetResumes(null);
                    if (resumes == null)
                    {
                        throw new ResumesException(ResumesErrors.NotFound);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        resumes = this.resumesService.GetResumes(skill);
                    }
                    else
                    {
                        resumes = this.resumesService.GetByName(name);
                    }

                    if (resumes.Length == 0)
                    {
                        throw new ResumesException(ResumesErrors.NotFound);
                    }
                }

                return new OkObjectResult(resumes);
            }
            catch (ResumesException ex)
            {
                var error = ex.Error;
                return error;
            }
            catch (System.Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}