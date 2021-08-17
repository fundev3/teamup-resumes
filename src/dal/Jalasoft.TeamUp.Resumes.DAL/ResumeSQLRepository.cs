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
            IEnumerable<Resume> resumes = new List<Resume>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                resumes = db.Query<Resume>("SELECT Id, Title, Sumary, CreationDate, LastUpdate FROM Resume");
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

        public Resume Update(Guid id, Resume updateObject)
        {
            var sql = "INSERT INTO ResumeSkill ( ResumeId, SkillId ) VALUES ( @idResume, @idSkill )";
            foreach (var skill in updateObject.Skills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@idResume", id, DbType.Guid);
                    parameter.Add("@idSkill", skill.Id, DbType.Int32);
                    db.Execute(sql, parameter);
                }
            }

            return updateObject;
        }

        public IEnumerable<Skill> AddSkills(IEnumerable<Skill> skills)
        {
            var sqlSave = "INSERT INTO Skill ( Name )  OUTPUT INSERTED.Id VALUES ( @Name )";
            foreach (var skill in skills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    var idNewSkill = db.QuerySingle<string>(sqlSave, skill);
                    skill.Id = idNewSkill;
                }
            }

            return skills;
        }
    }
}
