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

        public Postulation AddPostulation(Postulation postulation)
        {
            var storeProcedure = "Create_Postulation";
            var values = new
            {
                projectId = postulation.ProjectId,
                resumeId = postulation.ResumeId,
                projectName = postulation.ProjectName,
                resumeName = postulation.ResumeName,
                picture = postulation.Picture,
                creationDate = postulation.CreationDate,
                lastUpdate = postulation.LastUpdate,
                state = postulation.State
            };

            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                postulation.Id = db.QuerySingle<int>(storeProcedure, values, commandType: CommandType.StoredProcedure);
            }

            return postulation;
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

        public Postulation UpdatePostulation(Postulation updateObject)
        {
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Update_Postulation";
                var values = new
                {
                    id = updateObject.Id,
                    state = updateObject.State,
                    projectId = updateObject.ProjectId,
                    projectName = updateObject.ProjectName,
                    resumeName = updateObject.ResumeName,
                    picture = updateObject.Picture,
                };
                updateObject = db.QuerySingleOrDefault<Postulation>(sp, values, commandType: CommandType.StoredProcedure);
            }

            return updateObject;
        }
    }
}
