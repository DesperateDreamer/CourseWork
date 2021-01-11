using BLL.Models;
using BLL.Services.EntityServices;
using DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools;

namespace TestBLL
{
    public class BookTests
    {
        string prov = "books_tests.bin";
        BookService service = new BookService("books_tests.bin");
        BookService serviceTestJson = new BookService("empty_books_tests.json");
        BookService serviceTestXml = new BookService("empty_books_tests.xml");

        [Test]
        public void Test_BookAdding()
        {
            //Arrange
            BookDTO book = new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };

            //Act
            service.Add(book, prov);

            //Assert
            Assert.AreEqual(book.Author, Context.Books[0].Author);
            Assert.AreEqual(book.Title, Context.Books[0].Title);
        }

        [Test]
        public void Test_BookGetAll()
        {
            //Arrange
            BookDTO book1 = new BookDTO
            {
                Title = "Title1",
                Author = "Author1",
                IsInLibrary = true
            };
            service.Add(book1, prov);

            //Act
            List<BookDTO> act = service.GetAll(prov);

            //Assert
            Assert.AreEqual(book1.Author, Context.Books[1].Author);
            Assert.AreEqual(book1.Title, Context.Books[1].Title);


        }

        [Test]
        public void Test_BookGetByID()
        {
            //Arrange
            BookDTO book2 = new BookDTO
            {
                Title = "Title2",
                Author = "Author2",
                IsInLibrary = true
            };
            service.Add(book2, prov);

            //Act
            BookDTO act = service.GetByID(2, prov);

            //Assert
            Assert.AreEqual(book2.Author, Context.Books[2].Author);
            Assert.AreEqual(book2.Title, Context.Books[2].Title);
        }

        [Test]
        public void Test_BookSortByTitle()
        {
            //Arrange
            BookDTO exp = new BookDTO
            {
                Title = "Article",
                Author = "Ziam",
                IsInLibrary = true
            };
            service.Add(exp, prov);

            //Act
            List<BookDTO> act = service.SortByTitle(prov);

            //Assert
            Assert.AreEqual(exp.Author, act[0].Author);
        }

        [Test]
        public void Test_BookSortByAuthor()
        {
            //Arrange
            BookDTO exp = new BookDTO
            {
                Title = "Ziam",
                Author = "Art",
                IsInLibrary = true
            };
            service.Add(exp, prov);

            //Act
            List<BookDTO> act = service.SortByAuthor(prov);

            //Assert
            Assert.AreEqual(exp.Title, act[0].Title);
        }

        [Test]
        public void Test_BookUpdating()
        {
            //Arrange
            BookDTO book = new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            };
            BookDTO bookToUpdate = new BookDTO
            {
                Title = "TitleUpdate",
                Author = "AuthorUpdate",
                IsInLibrary = true
            };
            service.Add(book, prov);

            //Act
            service.Update(Context.Books.Count - 1, bookToUpdate, prov);

            //Assert
            Assert.AreEqual(bookToUpdate.Author,
                Context.Books[Context.Books.Count - 1].Author);
            Assert.AreEqual(bookToUpdate.Title,
                Context.Books[Context.Books.Count - 1].Title);
        }

        [Test]
        public void Test_BookDeleting()
        {
            //Arrange
            BookDTO bookDelete = new BookDTO
            {
                Title = "TitleDelete",
                Author = "AuthorDelete",
                IsInLibrary = true
            };
            service.Add(bookDelete, prov);
            int actCount = Context.Books.Count;

            //Act
            service.Delete(Context.Books.Count - 1, prov);

            //Assert
            Assert.AreEqual(actCount - 1, Context.Books.Count);
        }

        [Test]
        public void Test_BookSearch()
        {
            //Arrange
            List<BookDTO> exp = new List<BookDTO>();
            exp.Add(new BookDTO
            {
                Title = "Title",
                Author = "Author",
                IsInLibrary = true
            });

            //Act
            List<BookDTO> act = service.SearchBook(prov, "Title");

            //Assert
            Assert.AreEqual(exp[0].Title, act[0].Title);
        }

        [Test]
        public void Test_Book()
        {
            Assert.Throws<Exception>(() =>
                    service.SelectProvider("empty_books_tests.txt"));
        }
    }
}
