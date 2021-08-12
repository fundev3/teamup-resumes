namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillSQLRepository : ISkillSQLRepository
    {
        private string connectionString;

        public SkillSQLRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
        }

        public IEnumerable<Skill> AddSkills(IEnumerable<Skill> skills)
        {
            var sql = "SELECT Id FROM Skill WHERE Name=@name";
            var sqlSave = "INSERT INTO Skill ( Name )  OUTPUT INSERTED.Id VALUES ( @Name )";
            foreach (var skill in skills)
            {
                using (IDbConnection db = new SqlConnection(this.connectionString))
                {
                    db.Open();
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@name", skill.Name, DbType.String);
                    var id = db.QuerySingleOrDefault(sql, parameter);
                    if (id == null)
                    {
                        var idNewSkill = db.QuerySingle<int>(sqlSave, skill);
                        skill.Id = idNewSkill;
                    }
                }
            }

            return skills;
        }

        public IEnumerable<Skill> GetSkills(string name)
        {
            throw new NotImplementedException();
        }
    }
}
