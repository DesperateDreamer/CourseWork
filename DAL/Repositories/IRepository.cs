using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        void Add(TEntity entity, string nameOfFile);
        TEntity GetByID(int ID, string nameOfFile);
        List<TEntity> GetAll(string nameOfFile);
        void Delete(int ID, string nameOfFile);
        void Update(int ID, TEntity entity, string nameOfFile);
    }
}
