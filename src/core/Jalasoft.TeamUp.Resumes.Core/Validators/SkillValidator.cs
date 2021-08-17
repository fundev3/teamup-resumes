namespace Jalasoft.TeamUp.Resumes.Core.Validators
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using FluentValidation;
    using Jalasoft.TeamUp.Resumes.Models;

    public class SkillValidator : AbstractValidator<Skill>
    {
        private string connectionString;

        public SkillValidator()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
            this.RuleFor(x => x)
                .Must(this.FoundSkill).WithErrorCode("Skill not found.");
        }

        private bool FoundSkill(Skill skill)
        {
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sql = "SELECT Id FROM Skill WHERE EmsiId=@emsiId";
                db.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@emsiId", skill.EmsiId, DbType.String);
                var id = db.QuerySingleOrDefault(sql, parameter);
                if (id == null)
                {
                    return false;
                }

                skill.Id = id;
            }

            return true;
        }
    }
}
