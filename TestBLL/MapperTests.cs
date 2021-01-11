using BLL;
using BLL.Models;
using DAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBLL
{
    public class MapperTests
    {
        [Test]
        public void Test_BookMapToDTO()
        {
            //Arrange
            Book book = new Book
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };
            BookDTO bookDto = new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };

            //Act
            var mappedBookDTO = Mapper.ToDTO(book);

            //Assert
            Assert.AreEqual(bookDto.Title, mappedBookDTO.Title);
            Assert.AreEqual(bookDto.IsInLibrary, mappedBookDTO.IsInLibrary);
        }
        [Test]
        public void Test_BookMapFromDTO()
        {
            //Arrange
            Book book = new Book
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };
            BookDTO bookDto = new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };

            //Act
            var mappedBook = Mapper.FromDTO(bookDto);

            //Assert
            Assert.AreEqual(book.Title, mappedBook.Title);
            Assert.AreEqual(book.IsInLibrary, mappedBook.IsInLibrary);
        }

        [Test]
        public void Test_UserMapToDTO()
        {
            //Arrange
            Student student = new Student
            {
                FirstName = "Test",
                LastName = "Test",
                StudentID = "AA12345678",
                Group = 111,
                //Form = new ReaderForm
                //{
                //    BookCount = 0,
                //    Books = new List<Book>()
                //}
            };
            StudentDTO studentDto = new StudentDTO
            {
                FirstName = "Test",
                LastName = "Test",
                StudentID = "AA12345678",
                Group = 111,
                //FormDTO = new ReaderFormDTO
                //{
                //    BookCount = 0,
                //    Books = new List<BookDTO>()
                //}
            };

            //Act
            var mappedStudentDTO = Mapper.ToDTO(student);

            //Assert
            Assert.AreEqual(studentDto.FirstName, mappedStudentDTO.FirstName);
            Assert.AreEqual(studentDto.StudentID, mappedStudentDTO.StudentID);
            //Assert.AreEqual(studentDto.FormDTO.BookCount, mappedStudentDTO.FormDTO.BookCount);
        }
        [Test]
        public void Test_UserMapFromDTO()
        {
            //Arrange
            Student student = new Student
            {
                FirstName = "Test",
                LastName = "Test",
                StudentID = "AA12345678",
                Group = 111,
                //Form = new ReaderForm
                //{
                //    BookCount = 0,
                //    Books = new List<Book>()
                //}
            };
            StudentDTO studentDto = new StudentDTO
            {
                FirstName = "Test",
                LastName = "Test",
                StudentID = "AA12345678",
                Group = 111,
                //FormDTO = new ReaderFormDTO
                //{
                //    BookCount = 0,
                //    Books = new List<BookDTO>()
                //}
            };

            //Act
            var mappedStudent = Mapper.FromDTO(studentDto);

            //Assert
            Assert.AreEqual(student.FirstName, mappedStudent.FirstName);
            Assert.AreEqual(student.StudentID, mappedStudent.StudentID);
            //Assert.AreEqual(student.Form.BookCount, mappedStudent.Form.BookCount);
        }

        [Test]
        public void Test_ReaderFormMapToDTO()
        {
            //Arrange
            ReaderForm form = new ReaderForm
            {
                BookCount = 1,
                Books = new List<Book>()
            };
            ReaderFormDTO formDto = new ReaderFormDTO
            {
                BookCount = 1,
                Books = new List<BookDTO>()
            };
            form.Books.Add(new Book
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            });
            formDto.Books.Add(new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            });

            //Act
            var mappedFormDto = Mapper.ToDTO(form);

            //Assert
            Assert.AreEqual(formDto.BookCount, mappedFormDto.BookCount);
            Assert.AreEqual(formDto.Books[0].Author, mappedFormDto.Books[0].Author);
        }
        [Test]
        public void Test_ReaderFormMapFromDTO()
        {
            //Arrange
            ReaderForm form = new ReaderForm
            {
                BookCount = 1,
                Books = new List<Book>()
            };
            ReaderFormDTO formDto = new ReaderFormDTO
            {
                BookCount = 1,
                Books = new List<BookDTO>()
            };
            form.Books.Add(new Book
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            });
            formDto.Books.Add(new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            });

            //Act
            var mappedForm = Mapper.FromDTO(formDto);

            //Assert
            Assert.AreEqual(form.BookCount, mappedForm.BookCount);
            Assert.AreEqual(form.Books[0].Author, mappedForm.Books[0].Author);
        }
    }
}
