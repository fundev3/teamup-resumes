namespace Jalasoft.TeamUp.Resumes.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Jalasoft.TeamUp.Resumes.Models;

    public interface IRepository<T>
    {
        public T Add(T newObject);

        public T GetById(int id);

        public IEnumerable<T> GetAll();

        public void Update(int id, T updateObject);

        public void Delete(int id);

        public IEnumerable<Resume> GetByName(string name);
    }
}
