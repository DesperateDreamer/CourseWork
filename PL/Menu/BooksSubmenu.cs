using System;
using BLL.Services.EntityServices;
using BLL.Models;
using PL.IMenu;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Menu
{
    public class BooksSubmenu : ISubmenu
    {
        private static ReaderFormService formService;
        private static BookService bookService;
        private static StudentService studentService;
        private static string ReaderFile { get; set; }
        private static string SelectedProvider { get; set; }
        private static string NameOfFile { get; set; }
        private static string StudentFile { get; set; }

        private static void SetProvider()
        {
            Console.WriteLine(EnterFile);
            SelectedProvider = InputLogic.SelectProvider();
        }

        private static void SetNameOfFile()
        {
            ReaderFile = InputLogic.ReaderFile(SelectedProvider);
            NameOfFile = InputLogic.BooksLibrary(SelectedProvider);
            StudentFile = InputLogic.FileCheckIn(SelectedProvider);
        }

        public BooksSubmenu()
        {
            SetProvider();
            SetNameOfFile();
            studentService = new StudentService(StudentFile);
            formService = new ReaderFormService(ReaderFile, NameOfFile, StudentFile);
            bookService = new BookService(NameOfFile);
        }

        public void MainPage()
        {
            Console.WriteLine(MainCommands);
            switch (Console.ReadLine())
            {
                case "1":
                    Add();
                    MainPage();
                    break;
                case "2":
                    Delete();
                    MainPage();
                    break;
                case "3":
                    GetAll();
                    MainPage();
                    break;
                case "4":
                    Sort();
                    MainPage();
                    break;
                case "5":
                    ViewElement();
                    MainPage();
                    break;
                case "6":
                    Update();
                    MainPage();
                    break;
                case "7":
                    SearchBook();
                    MainPage();
                    break;
                case "8":
                    BookOwner();
                    MainPage();
                    break;
                case "9":
                    MainMenu.Welcome();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter command numb [1-9]");
                    Console.ResetColor();
                    MainPage();
                    break;
            }
        }

        public void Add()
        {
            try
            {
                Console.Clear();
                BookDTO book = CommandConsole.AddBook();
                Console.Clear();
                bookService.Add(book, NameOfFile);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully");
                Console.ResetColor();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        public void Delete()
        {
            try
            {
                Console.Clear();
                GetAll();
                int ID = GetBookID();
                bookService.Delete(ID, NameOfFile);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully");
                Console.ResetColor();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        public void GetAll()
        {
            Console.Clear();
            List<BookDTO> books = bookService.GetAll(NameOfFile);
            foreach (BookDTO book in books)
                Console.WriteLine($"{books.IndexOf(book)} {book.ToString()}");
        }

        public void Sort()
        {
            try
            {
                Console.Clear();
                Console.WriteLine(BooksSort);
                switch (Console.ReadLine())
                {
                    case "1":
                        List<BookDTO> sortByAuthor = bookService.SortByAuthor(NameOfFile);
                        foreach (BookDTO book in sortByAuthor)
                            Console.WriteLine($"{sortByAuthor.IndexOf(book)} {book.ToString()}");
                        break;
                    case "2":
                        List<BookDTO> sortByTitle = bookService.SortByTitle(NameOfFile);
                        foreach (BookDTO book in sortByTitle)
                            Console.WriteLine($"{sortByTitle.IndexOf(book)} {book.ToString()}");
                        break;
                    default:
                        Console.WriteLine("Enter sort number [1-2]");
                        Sort();
                        break;
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        public void ViewElement()
        {
            Console.Clear();
            GetAll();
            int ID = GetBookID();
            BookDTO book = bookService.GetByID(ID, NameOfFile);
            Console.WriteLine(book.ToString());
        }

        public void Update()
        {
            try
            {
                Console.Clear();
                GetAll();
                int ID = GetBookID();
                Console.WriteLine("Enter new book data");
                BookDTO book = CommandConsole.AddBook();
                bookService.Update(ID, book, NameOfFile);
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        private int GetBookID()
        {
            bool Parsing = false;
            int ID;
            do
            {
                Console.WriteLine("Select book");
                Parsing = int.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(Parsing ? "Waiting..." : "Enter book number [1-...]");
            } while (!Parsing);
            Console.Clear();
            return ID;
        }

        private void SearchBook()
        {
            Console.Clear();
            Console.WriteLine("Write keyword for book search");
            string key = Console.ReadLine();
            List<BookDTO> books = bookService.SearchBook(NameOfFile, key);

            foreach (BookDTO book in books)
                Console.WriteLine($"{books.IndexOf(book)} {book.ToString()}");
        }

        private void BookOwner()
        {
            Console.Clear();
            Console.WriteLine("Write keyword for book search among engaged");
            string key = Console.ReadLine();
            List<BookDTO> books = bookService.SearchBook(NameOfFile, key);


            List<BookDTO> engagedBooks = formService.SearchBook(ReaderFile, key);
            foreach (BookDTO book in engagedBooks)
                Console.WriteLine($"{engagedBooks.IndexOf(book)} {book.ToString()}");

            Console.WriteLine("Choose number of book to check owner");
            StudentDTO student = formService.WhoseForm(engagedBooks[GetBookID()], StudentFile, ReaderFile);
            Console.WriteLine("Book owner: ");
            Console.WriteLine($"{student.ToString()}");
        }

        private readonly static string EnterFile = "Please, select the serialize type with which you want to work";
        private readonly static string MainCommands = "Available commands:\n1. Add book\n2. Delete book\n3. Get a list of books" +
            "\n4. Sort the list of books\n5. View book information\n6. Change book data\n7. Search book\n8. Check book owner\n9. Back to welcome page";
        private readonly static string BooksSort = "Select sort type:\n1. Sort by author\n2. Sort by title";
    }
}
