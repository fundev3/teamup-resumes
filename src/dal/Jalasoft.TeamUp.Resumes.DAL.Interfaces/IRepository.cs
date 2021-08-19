namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        public T Add(T newObject);

        public T GetById(int id);

        public IEnumerable<T> GetAll();

        public T Update(T updateObject);

        public void Delete(int id);
    }
}
