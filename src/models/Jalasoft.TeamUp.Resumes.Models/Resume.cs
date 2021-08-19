namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;
    using System.Collections.Generic;

    public class Resume
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Person Person { get; set; }

        public Contact Contact { get; set; }

        public string Summary { get; set; }

        public List<Skill> Skills { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}
