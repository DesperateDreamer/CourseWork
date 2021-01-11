using BLL.Models;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL
{
    public static class Mapper
    {
        public static StudentDTO ToDTO(this Student student)
        {
            return new StudentDTO()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentID = student.StudentID,
                Group = student.Group
            };
        }

        public static Student FromDTO(this StudentDTO student)
        {
            return new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                StudentID = student.StudentID,
                Group = student.Group,
            };
        }

        public static ReaderFormDTO ToDTO(this ReaderForm form)
        {
            List<BookDTO> books = new List<BookDTO>();
            foreach (Book book in form.Books)
            {
                books.Add(ToDTO(book));                     //conversion from List<Book> to List<BookDTO>
            }
            return new ReaderFormDTO()
            {
                BookCount = form.BookCount,
                Books = books,
                ReaderID = form.ReaderID,
                FirstName = form.FirstName,
                LastName = form.LastName
            };
        }

        public static ReaderForm FromDTO(this ReaderFormDTO form)
        {
            List<Book> books = new List<Book>();
            foreach(BookDTO book in form.Books)
            {
                books.Add(FromDTO(book));                   //conversion from List<BookDTO> to List<Book>
            }
            return new ReaderForm()
            {
                BookCount = form.BookCount,
                Books = books,
                ReaderID = form.ReaderID,
                FirstName = form.FirstName,
                LastName = form.LastName
            };
        }

        public static BookDTO ToDTO(this Book book)
        {
            return new BookDTO()
            {
                Title = book.Title,
                Author = book.Author,
                IsInLibrary = book.IsInLibrary,
            };
        }

        public static Book FromDTO(this BookDTO book)
        {
            return new Book()
            {
                Title = book.Title,
                Author = book.Author,
                IsInLibrary = book.IsInLibrary,
            };
        }
    }
}
