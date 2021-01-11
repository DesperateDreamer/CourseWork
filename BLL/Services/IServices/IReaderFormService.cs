using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Models;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IReaderFormService
    {
        void Add(ReaderFormDTO form, string nameOfFile);
        void Delete(int ID, string nameOfFile);
        void Update(int ID, ReaderFormDTO form, string nameOfFile);
        ReaderFormDTO GetByID(int ID, string nameOfFile);
        List<ReaderFormDTO> GetAll(string nameOfFile);
        List<BookDTO> SearchBook(string nameOfFile, string key);
        ReaderFormDTO WhoseBook(BookDTO book, string nameOfFile);
        StudentDTO WhoseForm(BookDTO book, string stFile, string reFile);
    }
}
