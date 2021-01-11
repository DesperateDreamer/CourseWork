using System;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IBookService
    {
        void Add(BookDTO book, string nameOfFile);
        void Delete(int ID, string nameOfFile);
        List<BookDTO> SortByAuthor(string nameOfFile);
        List<BookDTO> SortByTitle(string nameOfFile);
        void Update(int ID, BookDTO book, string nameOfFile);
        BookDTO GetByID(int ID, string nameOfFile);
        List<BookDTO> GetAll(string nameOfFile);
    }
}
