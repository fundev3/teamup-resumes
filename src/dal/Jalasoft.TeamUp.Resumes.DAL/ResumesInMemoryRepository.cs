namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesInMemoryRepository : IResumesInMemoryRepository
    {
        private static readonly List<Resume> Resumes = new Resume[]
            {
                new Resume
                {
                    Id = 1,
                    Title = "My Custom Title",
                    PersonalInformation = new Person
                    {
                        FirstName = "Rodrigo",
                        LastName = "Baldivieso",
                        Birthdate = new DateTime(1995, 1, 1),
                        Picture = "?"
                    },
                    Contact = new Contact
                    {
                        Direction = "Tarija Av.",
                        Email = "rodrigo.baldivieso@fundacion-jala.org",
                        Phone = 77669911
                    },
                    Summary = "Rodrigo's summary",
                    Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = 1,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "API"
                        }
                    },
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now
                },
                new Resume
                {
                    Id = 2,
                    Title = "My Custom Title",
                    PersonalInformation = new Person
                    {
                        FirstName = "Paola",
                        LastName = "Quintanilla",
                        Birthdate = new DateTime(1995, 1, 1),
                        Picture = "?"
                    },
                    Contact = new Contact
                    {
                        Direction = "Cochabamba Av.",
                        Email = "paola.quintanilla@fundacion-jala.org",
                        Phone = 77669911
                    },
                    Summary = "Paola's summary",
                    Skills = new Skill[]
                    {
                        new Skill
                        {
                            Id = 1,
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            Name = "API"
                        }
                    },
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now
                }
            }.ToList();

        private static readonly List<Skill> Skills = new Skill[]
            {
                new Skill
                {
                    Id = 1,
                    EmsiId = "1",
                    Name = "c#"
                },
                new Skill
                {
                    Id = 2,
                    EmsiId = "2",
                    Name = "React"
                }
            }.ToList();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetAll()
        {
            return Resumes;
        }

        public Resume GetById(int id)
        {
            foreach (Resume item in Resumes)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Resume Add(Resume resume)
        {
            var resumes = new List<Resume>(Resumes) { resume }.ToArray();
            return resume;
        }

        public Resume Update(Resume updateObject)
        {
            var resume = Resumes.Find(x => x.Id == updateObject.Id);

            if (resume == null)
            {
                return resume;
            }

            var skills = resume.Skills.ToList();
            foreach (var skill in updateObject.Skills)
            {
                skills.Add(skill);
            }

            resume.Skills = skills.ToArray();
            return resume;
        }

        public IEnumerable<Skill> AddSkills(Skill[] skills)
        {
            foreach (var skill in skills)
            {
                Skills.Add(skill);
            }

            return skills;
        }

        public Skill SearchSkill(string emsiId)
        {
            return Skills.Find(x => x.EmsiId == emsiId);
        }
    }
}
