namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;
    using Microsoft.Extensions.Configuration;

    public class ResumeSQLRepository : IResumesRepository
    {
        private string connectionString;

        public ResumeSQLRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("DBConfig.json", optional: false, reloadOnChange: true)
            .Build();
            this.connectionString = config["appSettings:ConnectionString"];
        }

        public Resume Add(Resume newObject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            Resume resume = new Resume();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                resume = db.Query("delete from resume where id= " + id).Single();
            }
        }

        public List<Resume> GetAll()
        {
            List<Resume> resumes = new List<Resume>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                resumes = db.Query<Resume>("select * from resume").ToList();
            }

            return resumes;
        }

        public Resume GetById(Guid id)
        {
            Resume resume = new Resume();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                resume = db.Query("select * from resume where id= " + id).Single();
            }

            return resume;
        }

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
