namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillsInMemoryRepository : ISkillsRepository
    {
        private static readonly Skill[] Skills = new Skill[]
            {
                new Skill
                        {
                            Id = "KS1200B62W5ZF38RJ7TD",
                            Name = ".NET Framework"
                        },
                new Skill
                        {
                            Id = "ESA0523F4BCB6FF01890",
                            Name = "Cisco Networking"
                        },
                new Skill
                        {
                            Id = "KS126JW72Q0ST3JKR5K0",
                            Name = "Visual Basic .NET (Programming Language)"
                        },
            };

        public IEnumerable<Skill> GetSkills(string name)
        {
            return Skills;
        }
    }
}
