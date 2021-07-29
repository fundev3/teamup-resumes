namespace Jalasoft.TeamUp.Resumes.DAL
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using Jalasoft.TeamUp.Resumes.DAL.Interfaces;

    public class DapperWrapper : IDapperWrapper
    {
        public IEnumerable<T> Query<T>(IDbConnection dbConnection, string query)
        {
            return dbConnection.Query<T>(query);
        }
    }
}
