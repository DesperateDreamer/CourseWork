using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.IServices;
using DAL.Providers;
using DAL.Entities;
using DAL.Repositories;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Services.EntityServices
{
    public class ReaderFormService : IReaderFormService
    {
        ReaderFormRepository repository;
        StudentService studentService;
        BookService bookService;

        public ReaderFormService(string ReaderFile, string BookFile, string StudentFile)
        {
            repository = new ReaderFormRepository(SelectProvider(ReaderFile));
            studentService = new StudentService(StudentFile);
            bookService = new BookService(BookFile);
        }

        public void Add(ReaderFormDTO form, string nameOfFile)
        {
            repository.Add(Mapper.FromDTO(form), nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            repository.Delete(ID, nameOfFile);
        }

        public List<ReaderFormDTO> GetAll(string nameOfFile)
        {
            return repository.GetAll(nameOfFile).Select(x => Mapper.ToDTO(x)).ToList();
        }

        public ReaderFormDTO GetByID(int ID, string nameOfFile)
        {
            return Mapper.ToDTO(repository.GetByID(ID, nameOfFile));
        }

        public void Update(int ID, ReaderFormDTO form, string nameOfFile)
        {
            repository.Update(ID, Mapper.FromDTO(form), nameOfFile);
        }

        public void TakeBook(int IDBook, ReaderFormDTO form, string NameOfFile,
            int IDForm, string NameForm)
        {
            BookDTO book = bookService.GetByID(IDBook, NameOfFile);
            if (form.BookCount < 5)
            {
                form.Books.Add(book);
                form.BookCount++;
                repository.Update(IDForm, Mapper.FromDTO(form), NameForm);
                book.IsInLibrary = false;
                bookService.Update(IDBook, book, NameOfFile);
            }
            else
                throw new Exception();                          //ДОБАВИТЬ ИСКЛЮЧЕНИЕ
        }

        public void ReturnBook(int IDBook, ReaderFormDTO form, string ReaderFile,
            int IDForm, string BookFile, int IDinFile)
        {
            BookDTO book = form.Books[IDBook];
            form.Books.RemoveAt(IDBook);
            form.BookCount--;
            repository.Update(IDForm, Mapper.FromDTO(form), ReaderFile);
            bookService.Update(IDinFile, book, BookFile);
        }


        public List<BookDTO> SearchBook(string nameOfFile, string key)
        {
            return repository.GetAll(nameOfFile)
                .SelectMany(x => x.Books
                    .Where(c => c.Author.Contains(key)
                        || c.Title.Contains(key))
                .Select(b => Mapper.ToDTO(b)))
                .ToList();
        }

        public ReaderFormDTO WhoseBook(BookDTO book, string nameOfFile)
        {
            foreach (ReaderForm form in repository.GetAll(nameOfFile))
            {
                foreach (Book bookFor in form.Books)
                {
                    if (bookFor.Title == book.Title && bookFor.Author == book.Author)
                        return Mapper.ToDTO(form);
                }
            }
            throw new Exception();                          //ДОБАВИТЬ ИСКЛЮЧЕНИЕ
        }

        //
        public StudentDTO WhoseForm(BookDTO book, string stFile, string readFile)
        {
            ReaderFormDTO form = WhoseBook(book, readFile);
            List<StudentDTO> students = studentService.GetAll(stFile);
            StudentDTO returnedStudent = new StudentDTO();
            foreach (StudentDTO student in students)
            {
                if (student.StudentID == form.ReaderID)
                    returnedStudent = student;
                else
                    throw new Exception();                      //ДОБАВИТЬ ИСКЛЮЧЕНИЕ
            }
            return returnedStudent;
        }

        public ReaderFormDTO CreationForm(StudentDTO student)
        {
            ReaderFormDTO form = new ReaderFormDTO();
            form.ReaderID = student.StudentID;
            form.FirstName = student.FirstName;
            form.LastName = student.LastName;
            return form;
        }

        private IDataProvider<List<ReaderForm>> SelectProvider(string UserInput)
        {
            if (UserInput.EndsWith(".bin"))
                return new BinaryDataProvider<List<ReaderForm>>();
            else if (UserInput.EndsWith(".json"))
                return new JSONDataProvider<List<ReaderForm>>();
            else if (UserInput.EndsWith(".xml"))
                return new XMLDataProvider<List<ReaderForm>>();
            else
                throw new System.Exception();
        }
    }
}
