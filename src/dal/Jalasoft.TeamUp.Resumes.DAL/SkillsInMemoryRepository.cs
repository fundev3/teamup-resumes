namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillsInMemoryRepository : ISkillsRepository
    {
        private static readonly List<Skill> Skills = new List<Skill>
            {
                new Skill
                        {
                            Id = 1,
                            Name = ".NET Core",
                        },
                new Skill
                        {
                            Id = 2,
                            Name = "C#",
                        },
                new Skill
                        {
                            Id = 3,
                            Name = ".NET Framework",
                        }
            };

        public IEnumerable<Skill> GetSkills(string name)
        {
            return Skills;
        }
    }
}
