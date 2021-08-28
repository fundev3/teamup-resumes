namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Linq;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.Core.Validators;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class InvitationsService : IInvitationsService
    {
        private readonly IInvitationsRepository invitationsRepository;

        public InvitationsService(IInvitationsRepository invitationsRepository)
        {
            this.invitationsRepository = invitationsRepository;
        }

        public Guid UpdateInvitation(Guid invitationId)
        {
            return invitationId;
        }

        public Guid UpdateProject(int idResume, Guid projectId)
        {
            return projectId;
        }
    }
}
