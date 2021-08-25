namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class PostulationsRepository : IPostulationsRepository
    {
        private string connectionString;

        public PostulationsRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
        }

        public Postulation Add(Postulation postulation)
        {
            var storeProcedure = "Create_Postulation";
            var values = new
            {
                projectId = postulation.ProjectId,
                resumeId = postulation.ResumeId,
                projectName = postulation.ProjectName,
                resumeName = postulation.ResumeName,
                pictureName = postulation.Picture,
                startDate = postulation.StartDate,
                expireDate = postulation.ExpireDate,
                status = postulation.Status
            };

            // Postulation result = new Postulation();
            // var sql = "INSERT INTO Resume ( ProjectId, ResumeId, ProjectName, ResumeName, CreationDate, LastUpdate ) VALUES (@projectId, @resumeId, @projectName, @resumeName, @creationdate, @lastupdate)";
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                postulation.Id = db.QuerySingle<int>(storeProcedure, values, commandType: CommandType.StoredProcedure);

                // db.Open();
                // DynamicParameters parameter = new DynamicParameters();
                // parameter.Add("@projectId", postulation.ProjectId, DbType.Int32, ParameterDirection.Input, 30);
                // parameter.Add("@resumeId", postulation.ResumeId, DbType.Int32, ParameterDirection.Input, 30);
                // parameter.Add("@projectName", postulation.ProjectName, DbType.AnsiString, ParameterDirection.Input, 150);
                // parameter.Add("@resumeName", postulation.ResumeName, DbType.AnsiString, ParameterDirection.Input, 150);
                // parameter.Add("@resumeName", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                // parameter.Add("@lastupdate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                // db.Execute(sql, parameter);
            }

            return postulation;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Postulation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Postulation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Postulation Update(Postulation updateObject)
        {
            throw new NotImplementedException();
        }
    }
}
