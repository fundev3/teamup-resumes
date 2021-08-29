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
                picture = postulation.Picture,
                creationDate = DateTime.Now,
                lastUpdate = DateTime.Now,
                state = postulation.State
            };

            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                postulation.Id = db.QuerySingle<int>(storeProcedure, values, commandType: CommandType.StoredProcedure);
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

        public IEnumerable<Postulation> GetAllByProjectId(string projectId)
        {
            List<Postulation> postulations = new List<Postulation>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Postulations_Get_By_ProjectId";
                var values = new { ProjectId = projectId };
                postulations = db.Query<Postulation>(sp, values, commandType: CommandType.StoredProcedure).ToList();
            }

            return postulations;
        }

        public Postulation GetById(int id)
        {
            Postulation postulation;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Get_Postulation_By_Id";
                var values = new { id = id };
                postulation = db.QuerySingleOrDefault<Postulation>(sp, values, commandType: CommandType.StoredProcedure);
            }

            return postulation;
        }

        public IEnumerable<Resume> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Postulation> GetPostulationsByResumeId(int? resumeId)
        {
            List<Postulation> postulations = new List<Postulation>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Get_postulations_by_ld";
                var values = new { Id = resumeId };
                postulations = db.Query<Postulation>(sp, values, commandType: CommandType.StoredProcedure).ToList();
            }

            return postulations;
        }

        public Postulation Update(Postulation updateObject)
        {
            bool postulationExist = true;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Update_Postulation";
                var values = new
                {
                    id = updateObject.Id,
                    state = updateObject.State,
                    projectId = updateObject.ProjectId,
                    resumeId = updateObject.ResumeId,
                    projectName = updateObject.ProjectName,
                    resumeName = updateObject.ResumeName,
                    picture = updateObject.Picture,
                    creationDate = updateObject.CreationDate,
                    lastUpdate = updateObject.LastUpdate,
                };
                postulationExist = db.QuerySingle<bool>(sp, values, commandType: CommandType.StoredProcedure);
            }

            if (!postulationExist)
            {
                updateObject = null;
            }

            return updateObject;
        }
    }
}
