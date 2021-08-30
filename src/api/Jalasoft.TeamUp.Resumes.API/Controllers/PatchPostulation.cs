namespace Jalasoft.TeamUp.Resumes.API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.ResumesException;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
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
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The postulation identifier.")]
        [OpenApiRequestBody("application/json", typeof(JsonPatchDocument), Description = "JSON request body containing Postulation")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Postulation), Description = "Successful response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Resource not found")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Resource internal server error")]
        public async Task<IActionResult> Patch(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/postulations/{id:int}")] HttpRequest req, int id)
        {
            try
            {
                var postulation = new Postulation();
                postulation.Id = id;
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<JsonPatchDocument<Postulation>>(requestBody);
                data.ApplyTo(postulation);
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
            catch (Exception e)
            {
                var errorException = new ResumesException(ResumesErrors.InternalServerError, e);
                return errorException.Error;
            }
        }
    }
}
