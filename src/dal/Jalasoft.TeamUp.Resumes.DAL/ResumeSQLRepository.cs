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
    using Z.Dapper.Plus;

    public class ResumeSQLRepository : IResumesRepository
    {
        private string connectionString;

        public ResumeSQLRepository()
        {
            this.connectionString = Environment.GetEnvironmentVariable("SQLConnetionString", EnvironmentVariableTarget.Process);
        }

        public Resume Add(Resume newObject)
        {
            var sql = "INSERT INTO Resume ( Title, Summary, CreationDate, LastUpdate ) Output Inserted.Id VALUES (@title, @summary, @creationdate, @lastupdate)";
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Open();
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@title", newObject.Title, DbType.AnsiString, ParameterDirection.Input, 30);
                parameter.Add("@summary", newObject.Summary, DbType.AnsiString, ParameterDirection.Input, 150);
                parameter.Add("@creationdate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                parameter.Add("@lastupdate", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                newObject.Id = db.QuerySingle<int>(sql, parameter);
            }

            return newObject;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resume> GetAll()
        {
            IEnumerable<Resume> resumes;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                string sql = "SELECT resume.Id, resume.Title, resume.Summary, resume.CreationDate, resume.LastUpdate, resume.IdPerson, resume.IdContact, " +
                    "person.Id, person.FirstName, person.LastName, person.BirthDate, person.Picture, contact.Id, contact.Address, contact.Email, " +
                    "contact.Phone, resumeSkill.IdSkill, resumeSkill.IdResume, skill.Id, skill.Name " +
                    "FROM Resume resume " +
                    "INNER JOIN Person person ON resume.IdPerson = person.Id " +
                    "INNER JOIN Contact contact ON resume.IdContact = contact.Id " +
                    "INNER JOIN Resume_Skill resumeSkill ON resume.Id = resumeSkill.IdResume " +
                    "INNER JOIN Skill skill ON resumeSkill.IdSkill = skill.Id";

                var resumesAux = db.Query<Resume, Person, Contact, Skill, Resume>(sql, (resume, person, contact, skill) =>
                {
                    resume.Person = person;
                    resume.Contact = contact;
                    resume.Skills = new List<Skill>();
                    resume.Skills.Add(skill);
                    return resume;
                });

                resumes = resumesAux.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedResume = g.First();
                    groupedResume.Skills = g.Select(p => p.Skills.Single()).ToList();
                    return groupedResume;
                });
            }

            return resumes;
        }

        public Resume GetById(int id)
        {
            List<Resume> resume = new List<Resume>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sql = "SELECT res.Id, res.Title, res.Summary, res.CreationDate, res.LastUpdate, res.IdPerson, res.IdContact," +
                        "person.Id, person.FirstName, person.LastName, person.BirthDate, person.Picture, contact.Id, contact.Address, contact.Email," +
                        "contact.Phone, resSkill.IdSkill, resSkill.Idresume, skill.Id, skill.Name " +
                        "FROM resume res " +
                        "INNER JOIN Person person ON res.IdPerson = person.Id " +
                        "INNER JOIN Contact contact ON res.IdContact = contact.Id " +
                        "INNER JOIN resume_Skill resSkill ON res.Id = resSkill.Idresume " +
                        "INNER JOIN Skill skill ON resSkill.IdSkill = skill.Id " +
                        "WHERE res.Id = @id";
                db.Open();
                var parameters = new { id = id };
                var resumesAux = db.Query<Resume, Person, Contact, Skill, Resume>(
                    sql,
                    (resume, person, contact, skill) =>
                    {
                        resume.Person = person;
                        resume.Contact = contact;
                        resume.Skills = new List<Skill>();
                        resume.Skills.Add(skill);
                        return resume;
                    }, parameters);

                resume = resumesAux.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedResume = g.First();
                    groupedResume.Skills = g.Select(p => p.Skills.Single()).ToList();
                    return groupedResume;
                }).ToList();
            }

            return resume.ToList().Count != 0 ? resume[0] : null;
        }

        public Resume Update(Resume updateObject)
        {
            throw new NotImplementedException();
        }

        public Resume UpdateResumeSkill(int idResume, Skill[] skills)
        {
            var result = new Resume { Skills = skills.ToList() };
            var storeProcedure = "Resume_Skill_Update";
            var createTempTable = "CREATE TABLE #SkillTemp(Id Varchar(20), Name Varchar(50))";
            var value = new { idResume = idResume };
            bool resumeExist;
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                db.Open();
                db.Execute(createTempTable);
                DapperPlusManager.Entity<Skill>().Table("#SkillTemp");
                db.BulkInsert(skills);
                resumeExist = db.QuerySingle<bool>(storeProcedure, value, commandType: CommandType.StoredProcedure);
            }

            if (!resumeExist)
            {
                result = null;
            }

            return result;
        }

        public IEnumerable<Resume> GetByName(string name)
        {
            List<Resume> resumes = new List<Resume>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Resumes_Get_By_Name";
                var values = new { Title = name };
                resumes = db.Query<Resume>(sp, values, commandType: CommandType.StoredProcedure).ToList();
            }

            return resumes;
        }

        public IEnumerable<Resume> GetBySkill(string skill)
        {
            List<Resume> resumes = new List<Resume>();
            using (IDbConnection db = new SqlConnection(this.connectionString))
            {
                var sp = "Resumes_Get_By_Skill";
                var values = new { Skill = skill };
                resumes = db.Query<Resume>(sp, values, commandType: CommandType.StoredProcedure).ToList();
            }

            return resumes;
        }
    }
}
