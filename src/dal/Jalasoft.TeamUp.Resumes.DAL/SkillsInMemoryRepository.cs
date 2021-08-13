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
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = ".NET Core",
                        },
                new Skill
                        {
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = "C#",
                        },
                new Skill
                        {
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = ".NET Framework",
                        }
            };

        public IEnumerable<Skill> AddSkills(IEnumerable<Skill> skills)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skill> GetSkills(string name)
        {
            return Skills;
        }
    }
}
