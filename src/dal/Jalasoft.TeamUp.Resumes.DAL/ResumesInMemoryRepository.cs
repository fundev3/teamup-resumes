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
                    Id = new Guid("dd05d77a-ca64-401a-be39-8e1ea84e2f83"),
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
                            Id = 1,
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = 2,
                            NameSkill = "API"
                        }
                    },
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now
                },
                new Resume
                {
                    Id = new Guid("40b3f7e3-eaba-4b0f-bbef-5f5882af3ced"),
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
                            Id = 3,
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = 4,
                            NameSkill = "API"
                        }
                    },
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now
                }
            }.ToList();

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetAll()
        {
            return Resumes;
        }

        public Resume GetById(Guid id)
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

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
