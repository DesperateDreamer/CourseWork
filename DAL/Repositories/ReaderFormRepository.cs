using DAL.Entities;
using System.Collections.Generic;
using DAL.Providers;

namespace DAL.Repositories
{
    public class ReaderFormRepository : IRepository<ReaderForm>
    {
        DataProvider<List<ReaderForm>> provider;

        public ReaderFormRepository(IDataProvider<List<ReaderForm>> context)
        {
            provider = new DataProvider<List<ReaderForm>>(context);
            Context.ReaderForms = new List<ReaderForm>();
        }

        public void Add(ReaderForm entity, string nameOfFile)
        {
            Context.ReaderForms.Add(entity);
            provider.Serialize(Context.ReaderForms, nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            Context.ReaderForms = provider.Deserialize(nameOfFile);
            Context.ReaderForms.RemoveAt(ID);
            provider.Serialize(Context.ReaderForms, nameOfFile);
        }

        public List<ReaderForm> GetAll(string nameOfFile)
        {
            Context.ReaderForms = provider.Deserialize(nameOfFile);
            return Context.ReaderForms;
        }

        public ReaderForm GetByID(int ID, string nameOfFile)
        {
            Context.ReaderForms = provider.Deserialize(nameOfFile);
            return Context.ReaderForms[ID];
        }

        public void Update(int ID, ReaderForm entity, string nameOfFile)
        {
            Context.ReaderForms = provider.Deserialize(nameOfFile);
            Context.ReaderForms.RemoveAt(ID);
            Context.ReaderForms.Insert(ID, entity);
            provider.Serialize(Context.ReaderForms, nameOfFile);
        }
    }
}
