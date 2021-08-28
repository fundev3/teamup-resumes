namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IInvitationsService
    {
        Guid UpdateInvitation(Guid invitationId);

        Guid UpdateProject(int idResume, Guid projectId);
    }
}
