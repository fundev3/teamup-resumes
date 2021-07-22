namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;
    using Jalasoft.TeamUp.Projects.Models;

    public class Resume
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public Person Person { get; set; }

        public Contact Contact { get; set; }

        public string Summary { get; set; }

        public Skill Skills { get; set; }
    }
}
