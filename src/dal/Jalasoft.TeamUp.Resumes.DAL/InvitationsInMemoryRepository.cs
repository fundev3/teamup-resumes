namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class InvitationsInMemoryRepository : IInvitationsInMemoryRepository
    {
        private static readonly Invitation[] Invitations = new Invitation[]
            {
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-f5d8434584de",
                            ProjectId = "724d912-59de-44bd-a092-f5d8434584de",
                            ProjectName = "Sony",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a77c9"),
                            ResumeName = "Jose Ecos",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        },
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-111111111111",
                            ProjectId = "724d912-59de-44bd-a092-222222222222",
                            ProjectName = "Samsung",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a77c9"),
                            ResumeName = "Freddy",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        },
                new Invitation
                        {
                            Id = "5a7939fd-59de-44bd-a092-3333333333",
                            ProjectId = "724d912-59de-44bd-a092-4444444444",
                            ProjectName = "Huamei",
                            ResumeId = Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a7711"),
                            ResumeName = "Gustavo",
                            PictureResume = "photo.png",
                            TextInvitation = "We invite you to collaborate with the development team",
                            StartDate = DateTime.Now.AddDays(-10),
                            ExpireDate = DateTime.Now,
                            Status = "invited"
                        }
            };

        public IEnumerable<Invitation> GetInvitations(Guid id)
        {
            List<Invitation> foundInvitations = new List<Invitation>();
            foreach (var invitation in Invitations)
            {
                if (invitation.ResumeId == id)
                {
                    foundInvitations.Add(invitation);
                }
            }

            return foundInvitations;
        }
    }
}
