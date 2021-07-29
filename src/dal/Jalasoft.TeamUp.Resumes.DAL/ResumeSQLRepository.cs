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

    public class ResumeSQLRepository : IResumeRepository
    {
        private IDapperWrapper wrapper;
        private string connectionString;

        public ResumeSQLRepository(IDapperWrapper wrapper, string connectionString)
        {
            this.wrapper = wrapper;
            this.connectionString = connectionString;
        }

        public int Add(Resume newObject)
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
                resumes = this.wrapper.Query<Resume>(db, "select * from resume").ToList();
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
