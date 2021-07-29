namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumesRepository : IResumesRepository
    {
        private static readonly List<Resume> Resumes = new Resume[]
            {
                new Resume
                {
                    Id = new Guid("dd05d77a-ca64-401a-be39-8e1ea84e2f83"),
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
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = new Guid("0947a444-09c6-4281-894a-5e7a4acc38eb"),
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
                            Id = new Guid("184bf2b8-abc1-47da-b383-d0e05ca57d4d"),
                            NameSkill = "C#"
                        },
                        new Skill
                        {
                            Id = new Guid("0947a444-09c6-4281-894a-5e7a4acc38eb"),
                            NameSkill = "API"
                        }
                    },
                    CreationDate = DateTime.Now.AddDays(-10),
                    LastUpdate = DateTime.Now
                }
            }.ToList();

        public Resume Add(Resume newObject)
        {
            Resumes.Add(newObject);
            return Resumes.Last();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Resume> GetAll()
        {
            return Resumes.ToList();
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

        public IEnumerable<Resume> GetResumes()
        {
            return Resumes;
        }

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
