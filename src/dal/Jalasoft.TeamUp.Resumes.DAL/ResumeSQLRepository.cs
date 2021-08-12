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

    public class ResumeSQLRepository : IResumeSQLRepository
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

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetAll()
        {
            IEnumerable<Resume> resumes;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                string sql = "select * from Resume r join Person p on r.IdPerson = p.Id join Contact c on r.IdContact = c.Id join Resume_Skill rs on r.Id = rs.IdResume join Skill s on rs.IdSkill = s.Id ";

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

        public Resume GetById(Guid id)
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

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
