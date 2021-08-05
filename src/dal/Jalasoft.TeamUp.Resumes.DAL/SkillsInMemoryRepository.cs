﻿namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillsInMemoryRepository : ISkillsInMemoryRepository
    {
        private static readonly Skill[] Skills = new Skill[]
            {
                new Skill
                        {
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = "C#"
                        },
                new Skill
                        {
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = ".NET"
                        },
                new Skill
                        {
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = ".NET Core"
                        },
            };

        public Skill[] GetSkills(string name)
        {
            return Skills;
        }
    }
}