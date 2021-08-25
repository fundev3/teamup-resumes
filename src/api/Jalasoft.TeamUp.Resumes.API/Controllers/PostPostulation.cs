namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    public class PostPostulation
    {
        private readonly IPostulationsService postulationsService;

        public PostPostulation(IPostulationsService postulationsService)
        {
            this.postulationsService = postulationsService;
        }

        [FunctionName("PostPostulation")]
        [OpenApiOperation(operationId: "PostPostulation", tags: new[] { "Postulation" })]
        [OpenApiRequestBody("application/json", typeof(Postulation), Description = "JSON request body containing Postulation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Postulation), Description = "Successful response")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/postulations")] HttpRequest req)
        {
            try
            {
                Postulation createResume = new Postulation();
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                var input = JsonConvert.DeserializeObject<Postulation>(requestBody);
                createResume = this.postulationsService.PostPostulation(input);
                return new CreatedResult("v1/resume/{id:int}/postulation/:id", createResume);
            }
            catch (ValidationException exVal)
            {
                var errorException = new ResumesException(ResumesErrors.BadRequest, exVal);
                return errorException.Error;
            }
            catch (ResumesException e)
            {
                return e.Error;
            }
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
