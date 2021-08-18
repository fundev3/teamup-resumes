namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;
    using System.Collections.Generic;

    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmsiId { get; set; }

        public List<Resume> Resumes { get; set; }
    }
}
