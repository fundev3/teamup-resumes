namespace Jalasoft.TeamUp.Resumes.Models
{
    using System;
    using System.Collections.Generic;

    public class Skill
    {
        public int Id { get; set; }

        public string NameSkill { get; set; }

        public List<Resume> Resumes { get; set; }
    }
}
