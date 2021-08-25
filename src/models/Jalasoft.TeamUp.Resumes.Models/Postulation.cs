namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;

    public class Postulation
    {
        public int Id { get; set; }

        public Guid ProjectId { get; set; }

        public int ResumeId { get; set; }

        public string ProjectName { get; set; }

        public string ResumeName { get; set; }

        public string Picture { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public string Status { get; set; }
    }
}
