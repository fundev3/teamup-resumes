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
            IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
            .Build();
            this.connectionString = config["Values:ConnectionString"];
        }

        public Resume Add(Resume newObject)
        {
            var sql = "INSERT INTO Resume ( Id, Title, Sumary, CreationDate, LastUpdate ) VALUES (@id, @title, @sumary, @creationdate, @lastupdate)";
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@id", newObject.Id);
                parameter.Add("@title", newObject.Title);
                parameter.Add("@sumary", newObject.Summary);
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
                resumes = db.Query<Resume>("SELECT Id, Title, Sumary, CreationDate, LastUpdate FROM Resume").ToList();
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
                parameter.Add("@id", id);
                resume = db.QuerySingle(sql, parameter).ToSingle();
            }

            return resume;
        }

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
