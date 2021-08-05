namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillRepository : ISkillsRepository
    {
        private static readonly List<Skill> Skills = new Skill[]
            {
                new Skill
                        {
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = "C#"
                        }
            }.ToList();

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Skill> GetAll()
        {
            return Skills;
        }

        public Skill GetById(Guid id)
        {
            foreach (Skill item in Skills)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Guid GetSkillsByParameter(string skill)
        {
            foreach (Skill item in Skills)
            {
                if (item.NameSkill == skill)
                {
                    return item.Id;
                }
            }

            return Guid.NewGuid();
        }

        public Skill Add(Skill skill)
        {
            var skills = new List<Skill>(Skills) { skill }.ToArray();
            return skill;
        }

        public void Update(Guid id, Skill updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
