namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System.Collections.Generic;
    using System.Data;

    public interface IDapperWrapper
    {
        IEnumerable<T> Query<T>(IDbConnection connection, string query);
    }
}
