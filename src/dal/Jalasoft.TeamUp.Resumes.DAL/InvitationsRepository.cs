namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Newtonsoft.Json;
    using RestSharp;

    public class InvitationsRepository : IInvitationsRepository
    {
        private IResumesRepository resumesRepository;

        public InvitationsRepository()
        {
            this.resumesRepository = new ResumeSQLRepository();
        }

        private Guid UpdateInvitation(Guid invitationId)
        {
            var status = new { op = "replace", path = "/status", value = "Accepted" };
            var client = new RestClient($"https://fa-tuapi-projects-dev-bra.azurewebsites.net/api/v1/invitations/ {invitationId}");
            var request = new RestRequest(Method.PATCH);
            request.AddJsonBody(JsonConvert.SerializeObject(status));
            return invitationId;
        }

        private Guid UpdateProject(int idResume, Guid projectId)
        {
            Resume resume = this.resumesRepository.GetById(idResume);
            var project = new { op = "add", path = "/memberList", value = new { idResume = idResume, name = resume.Person.FirstName + " " + resume.Person.LastName } };
            var client = new RestClient($"https://fa-tuapi-projects-dev-bra.azurewebsites.net/api/v1/projects/ {idResume}");
            var request = new RestRequest(Method.PATCH);
            return projectId;
        }
    }
}