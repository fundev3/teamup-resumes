namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;

    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Picture { get; set; }
    }
}
