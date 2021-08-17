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

        public void Delete(int id)
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
            var sql = "INSERT INTO Resume_Skill ( IdResume, IdSkill ) VALUES ( @idResume, @idSkill )";
            foreach (var skill in updateObject.Skills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@idResume", updateObject.Id, DbType.Int32);
                    parameter.Add("@idSkill", skill.Id, DbType.Int32);
                    db.Execute(sql, parameter);
                }
            }

            return updateObject;
        }

        public IEnumerable<Skill> AddSkills(Skill[] skills)
        {
            var sql = "INSERT INTO Skill ( Name, EmsiId )  OUTPUT INSERTED.Id VALUES ( @name, @emsiId )";
            foreach (var skill in skills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@name", skill.Name, DbType.AnsiString);
                    parameter.Add("@emsiId", skill.EmsiId, DbType.AnsiString);
                    skill.Id = db.QuerySingle<int>(sql, parameter);
                }
            }

            return skills;
        }

        public Skill SearchSkill(string emsiId)
        {
            var skill = new Skill();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sql = "SELECT Id, EmsiId, Name FROM Skill WHERE EmsiId=@emsiId";
                db.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@emsiId", emsiId, DbType.AnsiString);
                skill = db.QuerySingleOrDefault<Skill>(sql, parameter);
            }

            return skill;
        }
    }
}
