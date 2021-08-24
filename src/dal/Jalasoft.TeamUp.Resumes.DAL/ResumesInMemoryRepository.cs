namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesInMemoryRepository : IResumesRepository
    {
        private static readonly List<Resume> Resumes = new Resume[]
            {
                new Resume
                {
                    Id = 1,
                    Title = "My Custom Title",
                    Person = new Person
                    {
                        FirstName = "Rodrigo",
                        LastName = "Baldivieso",
                        Birthdate = new DateTime(1995, 1, 1),
                        Picture = "?"
                    },
                    Contact = new Contact
                    {
                        Address = "Tarija Av.",
                        Email = "rodrigo.baldivieso@fundacion-jala.org",
                        Phone = 77669911
                    },
                    Summary = "Rodrigo's summary",
                    Skills = new List<Skill>()
                    {
                        new Skill
                        {
                            Id = "1",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "2",
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
                    Person = new Person
                    {
                        FirstName = "Paola",
                        LastName = "Quintanilla",
                        Birthdate = new DateTime(1995, 1, 1),
                        Picture = "?"
                    },
                    Contact = new Contact
                    {
                        Address = "Cochabamba Av.",
                        Email = "paola.quintanilla@fundacion-jala.org",
                        Phone = 77669911
                    },
                    Summary = "Paola's summary",
                    Skills = new List<Skill>()
                    {
                        new Skill
                        {
                            Id = "1",
                            Name = "C#"
                        },
                        new Skill
                        {
                            Id = "2",
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
                    Id = "1",
                    Name = "c#"
                },
                new Skill
                {
                    Id = "2",
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
            throw new NotImplementedException();
        }

        public IEnumerable<Skill> UpdateResumeSkill(int idResume, Skill[] skills)
        {
            var resume = Resumes.Find(x => x.Id == idResume);

            if (resume == null)
            {
                return new Skill[0];
            }

            var skillsUpdate = resume.Skills.ToList();
            foreach (var skill in skills)
            {
                skillsUpdate.Add(skill);
            }

            resume.Skills = skillsUpdate;
            return resume.Skills;
        }

        public IEnumerable<Resume> GetByName(string name)
        {
            return Resumes;
        }
    }
}
