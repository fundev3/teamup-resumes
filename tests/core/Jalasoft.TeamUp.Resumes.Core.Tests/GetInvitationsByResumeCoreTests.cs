namespace Jalasoft.TeamUp.Resumes.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Moq;
    using Xunit;

    public class GetInvitationsByResumeCoreTests
    {
        private readonly Mock<IInvitationsInMemoryRepository> mockRepository;
        private readonly InvitationsService invtitaionsService;

        public GetInvitationsByResumeCoreTests()
        {
            this.mockRepository = new Mock<IInvitationsInMemoryRepository>();
            this.invtitaionsService = new InvitationsService(this.mockRepository.Object);
        }

        public static IEnumerable<Invitation> GetTestInvitations()
        {
            var invitations = new List<Invitation>
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
            return invitations;
        }

        [Fact]
        public void GetInvitationsByResume_Returns_InvitationsList()
        {
            // Arrange
            var stubSkillList = GetTestInvitations();
            this.mockRepository.Setup(repository => repository.GetInvitations(Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a7711"))).Returns(stubSkillList);

            // Act
            var result = this.invtitaionsService.GetInvitations(Guid.Parse("fb14f3f4-7d2e-4c4e-9c50-9997c83a7711"));

            // Assert
            Assert.Equal(3, result.Length);
        }
    }
}
