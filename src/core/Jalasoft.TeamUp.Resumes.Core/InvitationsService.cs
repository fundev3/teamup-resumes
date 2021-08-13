namespace Jalasoft.TeamUp.Resumes.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.Core.Interfaces;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;

    public class InvitationsService : IInvitationsService
    {
        private readonly IInvitationsInMemoryRepository invitationsRepository;

        public InvitationsService(IInvitationsInMemoryRepository invitationsRepository)
        {
            this.invitationsRepository = invitationsRepository;
        }

        public Invitation[] GetInvitations(Guid id)
        {
            try
            {
                return this.invitationsRepository.GetInvitations(id).ToArray();
            }
            catch (Exception ex)
            {
                throw new ResumeException(ErrorsTypes.ServerError, ex);
            }
        }
    }
}
