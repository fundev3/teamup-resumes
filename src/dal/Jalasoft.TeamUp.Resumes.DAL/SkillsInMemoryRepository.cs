namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Jalasoft.TeamUp.Resumes.Utils.Exceptions;
    using Newtonsoft.Json;

    public class SkillsInMemoryRepository : ISkillsRepository
    {
        private static readonly Skill[] Skills = new Skill[]
            {
                new Skill
                        {
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = "C#"
                        },
                new Skill
                        {
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = ".NET"
                        },
                new Skill
                        {
                            Id = "184bf2b8-abc1-47da-b383-d0e05ca57d4d",
                            Name = ".NET Core"
                        },
            };

        public Skill[] GetSkills(string name)
        {
            return Skills;
        }
    }
}
