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

    public class PatchPostulation
    {
        private readonly IPostulationsService postulationsService;

        public PatchPostulation(IPostulationsService postulationsService)
        {
            this.postulationsService = postulationsService;
        }

        [FunctionName("PatchPostulation")]
        [OpenApiOperation(operationId: "PatchPostulation", tags: new[] { "Postulations" })]
        [OpenApiRequestBody("application/json", typeof(Postulation), Description = "JSON request body containing Postulation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Postulation), Description = "Successful response")]
        public IActionResult Patch(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/postulations")] HttpRequest req)
        {
            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var postulation = JsonConvert.DeserializeObject<Postulation>(requestBody);

                var result = this.postulationsService.PatchPostulation(postulation);

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
