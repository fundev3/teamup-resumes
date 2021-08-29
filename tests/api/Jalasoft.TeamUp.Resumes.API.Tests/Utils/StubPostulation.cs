namespace Jalasoft.TeamUp.Resumes.API.Tests.Utils
{
    using System;
    using Jalasoft.TeamUp.Resumes.Models;

    public class StubPostulation
    {
        public static Postulation GetPostulation()
        {
            Postulation stubPostulation = new Postulation
            {
                Id = 1,
                Picture = "Test.png",
                ProjectId = "123",
                ResumeId = 1,
                ProjectName = "TeamUp",
                ResumeName = "Daniela Zeballos",
                State = "Accepted",
                LastUpdate = DateTime.Now,
                CreationDate = DateTime.Today.AddDays(-10),
            };
            return stubPostulation;
        }
    }
}
