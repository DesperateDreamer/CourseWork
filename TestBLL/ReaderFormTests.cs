using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using BLL.Models;
using BLL.Services.EntityServices;
using DAL;
using NUnit.Framework;

namespace TestBLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL;
    using BLL.Models;
    using BLL.Services.EntityServices;
    using DAL;
    using NUnit.Framework;

    namespace TestBLL
    {
        public class ReaderFormTests
        {
            string prov = "readerforms_tests.bin";
            string provBooks = "readerforms_books_tests.bin";
            string provStudents = "readerforms_students_tests.bin";
            ReaderFormService service = new ReaderFormService("readerforms_tests.bin",
                "readerforms_books_tests.bin", "readerforms_students_tests.bin");
            //ReaderFormService serviceTestJson = new ReaderFormService("empty_readerforms_tests.json",);
            //ReaderFormService serviceTestXml = new ReaderFormService("empty_readerforms_tests.xml");
            BookService bookService = new BookService("readerforms_books_tests.bin");
            StudentService studentService = new StudentService("readerforms_students_tests.bin");

            [Test]
            public void Test_FormAdd()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                studentService.Add(student, provStudents);

                //Act
                service.Add(form, prov);

                //Assert
                Assert.AreEqual(form.ReaderID, Context.ReaderForms[0].ReaderID);
            }

            [Test]
            public void Test_FormGetAll()
            {
                //Arrange
                StudentDTO student1 = new StudentDTO
                {
                    FirstName = "First1",
                    LastName = "Last1",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form1 = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student1.FirstName,
                    LastName = student1.LastName,
                    ReaderID = student1.StudentID
                };
                studentService.Add(student1, provStudents);
                service.Add(form1, prov);

                //Act
                List<ReaderFormDTO> act = service.GetAll(prov);

                //Assert
                Assert.AreEqual(act[1].FirstName, Context.ReaderForms[1].FirstName);
            }

            [Test]
            public void Test_FormGetByID()
            {
                //Arrange
                StudentDTO student2 = new StudentDTO
                {
                    FirstName = "First2",
                    LastName = "Last2",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form2 = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student2.FirstName,
                    LastName = student2.LastName,
                    ReaderID = student2.StudentID
                };
                studentService.Add(student2, provStudents);
                service.Add(form2, prov);

                //Act
                ReaderFormDTO act = service.GetByID(2, prov);

                //Assert
                Assert.AreEqual(act.FirstName, Context.ReaderForms[2].FirstName);
            }

            [Test]
            public void Test_FormUpdate()
            {
                //Arrange
                StudentDTO student2 = new StudentDTO
                {
                    FirstName = "First2",
                    LastName = "Last2",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form2 = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student2.FirstName,
                    LastName = student2.LastName,
                    ReaderID = student2.StudentID
                };
                ReaderFormDTO formToUpdate = new ReaderFormDTO
                {
                    BookCount = 2,
                    Books = new List<BookDTO>(),
                    FirstName = student2.FirstName,
                    LastName = student2.LastName,
                    ReaderID = student2.StudentID
                };
                studentService.Add(student2, provStudents);
                service.Add(form2, prov);

                //Act
                service.Update(Context.ReaderForms.Count - 2, formToUpdate, prov);

                //Assert
                Assert.AreEqual(formToUpdate.BookCount,
                    Context.ReaderForms[Context.Students.Count - 3].BookCount);
            }

            [Test]
            public void Test_FormDelete()
            {
                //Arrange
                StudentDTO student2 = new StudentDTO
                {
                    FirstName = "First2",
                    LastName = "Last2",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO formDelete = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student2.FirstName,
                    LastName = student2.LastName,
                    ReaderID = student2.StudentID
                };
                service.Add(formDelete, prov);
                int actCount = Context.ReaderForms.Count;

                //Act
                service.Delete(Context.ReaderForms.Count - 1, prov);

                //Assert
                Assert.AreEqual(actCount - 1, Context.ReaderForms.Count);
            }

            [Test]
            public void Test_FormSearchBook()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "SD43211234",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 1,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                BookDTO book = new BookDTO
                {
                    Author = "Test",
                    Title = "Asses",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);
                studentService.Add(student, provStudents);
                service.Add(form, prov);
                service.TakeBook(0, form, provBooks, 0, prov);

                //Act
                List<BookDTO> booksSearch = service.SearchBook(prov, "s");

                //Assert
                Assert.AreEqual(booksSearch.FirstOrDefault().Title, Mapper.ToDTO(Context.Books[0]).Title);
            }

            [Test]
            public void Test_FormWhoseForm()
            {
                StudentDTO student = new StudentDTO
                {
                    FirstName = "Dru",
                    LastName = "Dima",
                    StudentID = "AA99999999",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 1,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                studentService.Add(student, provStudents);
                service.Add(form, prov);
                BookDTO book = new BookDTO
                {
                    Title = "Adventure",
                    Author = "Auf",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);
                service.TakeBook(Context.Books.Count - 1, form, provBooks,
                    Context.ReaderForms.Count - 1, prov);

                //Act & Assert
                Assert.Throws<Exception>(() =>
                    service.WhoseForm(book, provStudents, prov));
            }

            [Test]
            public void Test_TakeBook()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 1,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                studentService.Add(student, provStudents);
                service.Add(form, prov);
                BookDTO book = new BookDTO
                {
                    Title = "Title",
                    Author = "Author",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);

                //Act
                service.TakeBook(1, form, provBooks, 5, prov);

                //Assert
                Assert.AreEqual(2, form.BookCount);
                Assert.AreEqual("Auf", form.Books[0].Author);
            }

            [Test]
            public void Test_GetAll()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 0,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                studentService.Add(student, provStudents);
                service.Add(form, prov);
                BookDTO book = new BookDTO
                {
                    Title = "Title",
                    Author = "Author",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);
                BookDTO book2 = new BookDTO
                {
                    Title = "Title",
                    Author = "Author",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);

                //Act
                service.TakeBook(1, form, provBooks, 4, prov);
                service.TakeBook(2, form, provBooks, 5, prov);
                //Arrange

                //Assert
                Assert.AreEqual(2, form.BookCount);
                Assert.AreEqual("Auf", form.Books[0].Author);
                Assert.AreEqual("Author", form.Books[1].Author);
            }

            [Test]
            public void Test_ReturnBook()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "AA12345678",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 1,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID
                };
                studentService.Add(student, provStudents);
                service.Add(form, prov);
                BookDTO book = new BookDTO
                {
                    Title = "Title",
                    Author = "Author",
                    IsInLibrary = true
                };
                bookService.Add(book, provBooks);

                service.TakeBook(0, form, provBooks, 0, prov);

                //Act
                service.ReturnBook(0, form, prov, 0, provBooks, 0);

                //Assert
                Assert.AreEqual(1, form.BookCount);
            }

            [Test]
            public void Test_FormCreation()
            {
                //Arrange
                StudentDTO student = new StudentDTO
                {
                    FirstName = "First",
                    LastName = "Last",
                    StudentID = "SD55555555",
                    Group = 300,
                };
                ReaderFormDTO form = new ReaderFormDTO
                {
                    BookCount = 1,
                    Books = new List<BookDTO>(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = "SD55555555"
                };
                studentService.Add(student, provStudents);
                string act = Context.Students.Select(x => x.StudentID)
                    .Single(x => x == student.StudentID);
                //.Select(x => x.StudentID).Single(x => x);

                //Act
                service.CreationForm(student);

                //Assert
                Assert.AreEqual(form.ReaderID, act);
            }
        }
    }
}
