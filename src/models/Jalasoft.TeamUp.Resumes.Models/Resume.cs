namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;
    using System.Collections.Generic;

    public class Resume
    {
        public Guid Id { get; set; } // B error en ResumeServiceTest

        public string Title { get; set; } // B

        public Person Person { get; set; } // referenciado con Person

        public Contact Contact { get; set; } // referenciado con Contact

        public string Summary { get; set; } // B

        public List<Skill> Skills { get; set; } // No

        public DateTime CreationDate { get; set; } // B

        public DateTime LastUpdate { get; set; } // B
    }
}
