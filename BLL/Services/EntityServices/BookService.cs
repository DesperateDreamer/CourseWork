using DAL.Repositories;
using DAL.Entities;
using BLL.Models;
using BLL.Services.IServices;
using DAL.Providers;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services.EntityServices
{
    public class BookService : IBookService
    {
        BookRepository repository;
        public BookService(string UserInput)
        {
            repository = new BookRepository(SelectProvider(UserInput));
        }

        public void Add(BookDTO book, string nameOfFile)
        {
            repository.Add(Mapper.FromDTO(book), nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            repository.Delete(ID, nameOfFile);
        }

        public List<BookDTO> GetAll(string nameOfFile)
        {
            //List<BookDTO> booksDTO = new List<BookDTO>();
            //List<Book> books = repository.GetAll(nameOfFile);
            //foreach(Book book in books)
            //    booksDTO.Add(Mapper.ToDTO(book));
            //return booksDTO;
            return repository.GetAll(nameOfFile).Select(x => Mapper.ToDTO(x)).ToList();
        }

        public BookDTO GetByID(int ID, string nameOfFile)
        {
            //Book book = repository.GetByID(ID, nameOfFile);
            return Mapper.ToDTO(repository.GetByID(ID, nameOfFile));
        }

        public List<BookDTO> SortByAuthor(string nameOfFile)
        {
            return GetAll(nameOfFile).OrderBy(x => x.Author).ToList();
        }

        public List<BookDTO> SortByTitle(string nameOfFile)
        {
            return GetAll(nameOfFile).OrderBy(x => x.Title).ToList();
        }

        public void Update(int ID, BookDTO book, string nameOfFile)
        {
            repository.Update(ID, Mapper.FromDTO(book), nameOfFile);
        }

        public List<BookDTO> SearchBook(string nameOfFile, string key)
        {
            return repository.GetAll(nameOfFile)
                .Where(x => x.Title.Contains(key)
                    || x.Author.Contains(key))
                .Select(x => Mapper.ToDTO(x))
                .ToList();
        }

        public IDataProvider<List<Book>> SelectProvider(string UserInput)
        {
            if (UserInput.EndsWith(".bin"))
                return new BinaryDataProvider<List<Book>>();
            else if (UserInput.EndsWith(".json"))
                return new JSONDataProvider<List<Book>>();
            else if (UserInput.EndsWith(".xml"))
                return new XMLDataProvider<List<Book>>();
            else
                throw new System.Exception();
        }
    }
}
