namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        public T Add(T newObject);

        public T GetById(Guid id);

        public IEnumerable<T> GetAll();

        public void Update(Guid id, T updateObject);

        public void Delete(Guid id);
    }
}
