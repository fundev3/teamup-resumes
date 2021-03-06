namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

    public class PutResumeSkill
    {
        private readonly IResumesService resumesService;

        public PutResumeSkill(IResumesService resumesService)
        {
            this.resumesService = resumesService;
        }

        [FunctionName("UpdateResumeSkill")]
        [OpenApiOperation(operationId: "UpdateResumeSkill", tags: new[] { "Resumes" })]
        [OpenApiRequestBody("application/json", typeof(Skill[]), Description = "JSON request body containing list of skills")]
        [OpenApiParameter(name: "idResume", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The resume identifier.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Skill[]), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Resource bad request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public IActionResult UpdateResumeSkill(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/resumes/{idResume}/skills")] HttpRequest req, int idResume)
        {
            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var skills = JsonConvert.DeserializeObject<Skill[]>(requestBody);

                var result = this.resumesService.UpdateResumeSkill(idResume, skills);

                if (result == null)
                {
                    throw new ResumesException(ResumesErrors.NotFound);
                }

                return new OkObjectResult(result);
            }
            catch (ResumesException e)
            {
                return e.Error;
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
