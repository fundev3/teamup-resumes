namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.Extensions.Configuration;
    using Z.Dapper.Plus;

    public class ResumeSQLRepository : IResumesRepository
    {
        private string connectionString;

        public ResumeSQLRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
        }

        public Resume Add(Resume newObject)
        {
            var sql = "INSERT INTO Resume ( Id, Title, Sumary, CreationDate, LastUpdate ) VALUES (@id, @title, @summary, @creationdate, @lastupdate)";
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Open();
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@id", newObject.Id, DbType.Int32);
                parameter.Add("@title", newObject.Title, DbType.AnsiString, ParameterDirection.Input, 30);
                parameter.Add("@summary", newObject.Summary, DbType.AnsiString, ParameterDirection.Input, 150);
                parameter.Add("@creationdate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                parameter.Add("@lastupdate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                db.Execute(sql, parameter);
            }

            return newObject;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetAll()
        {
            IEnumerable<Resume> resumes;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                string sql = "SELECT resume.Id, resume.Title, resume.Sumary, resume.CreationDate, resume.LastUpdate, resume.IdPerson, resume.IdContact, " +
                    "person.Id, person.FirstName, person.LastName, person.BirthDate, person.Picture,	contact.Id, contact.Address, contact.Email, " +
                    "contact.Phone, resumeSkill.IdSkill, resumeSkill.IdResume, skill.Id, skill.EmsiId, skill.Name " +
                    "FROM Resume resume " +
                    "INNER JOIN Person person ON resume.IdPerson = person.Id " +
                    "INNER JOIN Contact contact ON resume.IdContact = contact.Id " +
                    "INNER JOIN Resume_Skill resumeSkill ON resume.Id = resumeSkill.IdResume " +
                    "INNER JOIN Skill skill ON resumeSkill.IdSkill = skill.Id";

                var resumesAux = db.Query<Resume, Person, Contact, Skill, Resume>(sql, (resume, person, contact, skill) =>
                {
                    resume.Person = person;
                    resume.Contact = contact;
                    resume.Skills = new List<Skill>();
                    resume.Skills.Add(skill);
                    return resume;
                });

                resumes = resumesAux.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedResume = g.First();
                    groupedResume.Skills = g.Select(p => p.Skills.Single()).ToList();
                    return groupedResume;
                });
            }

            return resumes;
        }

        public Resume GetById(int id)
        {
            var sql = "SELECT Id, Title, Sumary, CreationDate, LastUpdate FROM Resume WHERE Id=@id";
            Resume resume = new Resume();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", id, DbType.Int32);
                resume = db.QuerySingle(sql, parameter);
            }

            return resume;
        }

        public Resume Update(Resume updateObject)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skill> UpdateResumeSkill(int idResume, Skill[] skills)
        {
            var storeProcedure = "Resume_Skill_Update";
            var createTempTable = "CREATE TABLE #SkillTemp(Id INT, Name Varchar(20))";
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Execute(createTempTable);
                DapperPlusManager.Entity<Skill>().Table("#SkillTemp");
                db.BulkInsert(skills);
                db.Query(storeProcedure, commandType: CommandType.StoredProcedure);
            }

            return skills;
        }
    }
}
