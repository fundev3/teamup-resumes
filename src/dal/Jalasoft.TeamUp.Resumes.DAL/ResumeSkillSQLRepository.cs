namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class ResumeSkillSQLRepository : IResumeSkillSQLRepository
    {
        private string connectionString;

        public ResumeSkillSQLRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
        }

        public IEnumerable<ResumeSkill> AddResumesSkills(IEnumerable<ResumeSkill> resumeSkills)
        {
            var sql = "INSERT INTO ResumeSkill ( ResumeId, SkillId ) VALUES ( @idResume, @idSkill )";
            foreach (var skill in resumeSkills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@idResume", skill.ResumeId, DbType.Guid);
                    parameter.Add("@idSkill", skill.SkillId, DbType.Int32);
                    db.Execute(sql, parameter);
                }
            }

            return resumeSkills;
        }
    }
}
