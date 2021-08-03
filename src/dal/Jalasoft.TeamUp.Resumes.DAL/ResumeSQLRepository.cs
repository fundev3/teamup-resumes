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

    public class ResumeSQLRepository : IResumeSQLRepository
    {
        private string connectionString;

        public ResumeSQLRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
            .Build();
            this.connectionString = config["appSettings:ConnectionString"];
        }

        public Resume Add(Resume newObject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(Guid id, Resume updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
