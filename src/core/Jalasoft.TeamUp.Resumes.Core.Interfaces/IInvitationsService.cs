namespace Jalasoft.TeamUp.Resumes.Core.Interfaces
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IInvitationsService
    {
        Invitation[] GetInvitations(Guid id);
    }
}