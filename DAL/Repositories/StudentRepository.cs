using System.Collections.Generic;
using DAL.Providers;
using DAL.Entities;

namespace DAL.Repositories
{
    public class StudentRepository : IRepository<Student>
    {
        DataProvider<List<Student>> provider;
        public StudentRepository(IDataProvider<List<Student>> context)
        {
            provider = new DataProvider<List<Student>>(context);
            Context.Students = new List<Student>();
        }
        public void Add(Student entity, string nameOfFile)
        {
            Context.Students.Add(entity);
            provider.Serialize(Context.Students, nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            Context.Students = provider.Deserialize(nameOfFile);
            Context.Students.RemoveAt(ID);
            provider.Serialize(Context.Students, nameOfFile);
        }

        public List<Student> GetAll(string nameOfFile)
        {
            Context.Students = provider.Deserialize(nameOfFile);
            return Context.Students;
        }

        public Student GetByID(int ID, string nameOfFile)
        {
            Context.Students = provider.Deserialize(nameOfFile);
            return Context.Students[ID];
        }

        public void Update(int ID, Student entity, string nameOfFile)
        {
            Context.Students = provider.Deserialize(nameOfFile);
            Context.Students.RemoveAt(ID);
            Context.Students.Insert(ID, entity);
            provider.Serialize(Context.Students, nameOfFile);
        }
    }
}
