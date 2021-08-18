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
                            EmsiId = "KS120P86XDXZJT3B7KVJ",
                            Name = ".NET Framework"
                        },
                new Skill
                        {
                            Id = 2,
                            EmsiId = "KS122P86XDXZJT3B7KVJ",
                            Name = "Cisco Networking"
                        },
                new Skill
                        {
                            Id = 3,
                            EmsiId = "KS121P86XDXZJT3B7KVJ",
                            Name = "Visual Basic .NET (Programming Language)"
                        },
            };

        public IEnumerable<Skill> GetSkills(string name)
        {
            return Skills;
        }
    }
}
