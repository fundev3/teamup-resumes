namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        public int Add(T newObject);

        public T GetById(Guid id);

        public List<T> GetAll();

        public void Update(Guid id, T updateObject);

        public void Delete(Guid id);
    }
}
