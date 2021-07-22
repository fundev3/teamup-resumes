namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;

    public class Resume
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Person PersonalInformation { get; set; }

        public Contact Contact { get; set; }

        public string Summary { get; set; }

        public Skill[] Skills { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
