namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;

    public class Resume
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Summary { get; set; }
    }
}
