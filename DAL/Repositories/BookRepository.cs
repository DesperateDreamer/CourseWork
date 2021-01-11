using DAL.Entities;
using DAL.Providers;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        DataProvider<List<Book>> provider;

        public BookRepository(IDataProvider<List<Book>> context)
        {
            provider = new DataProvider<List<Book>>(context);
            Context.Books = new List<Book>();
        }

        public void Add(Book entity, string nameOfFile)
        {
            Context.Books.Add(entity);
            provider.Serialize(Context.Books, nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            Context.Books = provider.Deserialize(nameOfFile);
            Context.Books.RemoveAt(ID);
            provider.Serialize(Context.Books, nameOfFile);
        }

        public List<Book> GetAll(string nameOfFile)
        {
            Context.Books = provider.Deserialize(nameOfFile);
            return Context.Books;
        }

        public Book GetByID(int ID, string nameOfFile)
        {
            Context.Books = provider.Deserialize(nameOfFile);
            return Context.Books[ID];
        }

        public void Update(int ID, Book entity, string nameOfFile)
        {
            Context.Books = provider.Deserialize(nameOfFile);
            Context.Books.RemoveAt(ID);
            Context.Books.Insert(ID, entity);
            provider.Serialize(Context.Books, nameOfFile);
        }
    }
}
