using System;
using PL.IMenu;
using BLL.Services.EntityServices;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Menu
{
    public class StudentSubmenu : ISubmenu
    {
        private static ReaderFormService formService;
        private static StudentService studentService;
        private static BookService bookService;
        private static StudentDTO Student;
        private static ReaderFormDTO Form;
        private static string SelectedProvider { get; set; }
        private static string StudentFile { get; set; }
        private static string ReaderFile { get; set; }
        private static string BookFile { get; set; }

        private static void SetProvider()
        {
            Console.WriteLine(EnterFile);
            SelectedProvider = InputLogic.SelectProvider();
        }
        private static void SetNameOfFile()
        {
            StudentFile = InputLogic.FileCheckIn(SelectedProvider);
            ReaderFile = InputLogic.ReaderFile(SelectedProvider);
            BookFile = InputLogic.BooksLibrary(SelectedProvider);
        }

        public StudentSubmenu()
        {
            SetProvider();
            SetNameOfFile();
            studentService = new StudentService(StudentFile);
            formService = new ReaderFormService(ReaderFile, BookFile, StudentFile);
            bookService = new BookService(BookFile);
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
                    TakeBook();
                    MainPage();
                    break;
                case "8":
                    ReturnBook();
                    MainPage();
                    break;
                case "9":
                    SearchStudent();
                    MainPage();
                    break;
                case "0":
                    MainMenu.Welcome();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter command numb [0-9]");
                    Console.ResetColor();
                    MainPage();
                    break;
            }
        }

        public void WelcomePage()
        {
            Console.WriteLine("Are you a registered user? +/-");
            switch (Console.ReadLine())
            {
                case "+":
                    CheckInPage();
                    MainPage();
                    break;
                case "-":
                    NewUserPage();
                    MainPage();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("+ if you are registered\n- if you aren't registered");
                    Console.ResetColor();
                    WelcomePage();
                    break;
            }
        }

        private void CheckInPage()
        {
            string StudentID = InputLogic.InputID("student ID");
            if (studentService.CheckIn(StudentID, StudentFile) == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ErrorCheckIn);
                Console.ResetColor();
                NewUserPage();
            }
            else
            {
                Student = studentService.CheckIn(StudentID, StudentFile);
                Form = formService.CreationForm(Student);
            }
        }

        private void NewUserPage()
        {
            Student = CommandConsole.AddStudent();
            Form = formService.CreationForm(Student);
            studentService.Add(Student, StudentFile);
            formService.Add(Form, ReaderFile);
        }

        public void Add()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Please, enter data for new account");
                NewUserPage();
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
                int ID = GetStudentID();
                studentService.Delete(ID, StudentFile);
                formService.Delete(ID, ReaderFile);
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
            List<StudentDTO> studentsDTO = studentService.GetAll(StudentFile);
            foreach (StudentDTO student in studentsDTO)
                Console.WriteLine($"{studentsDTO.IndexOf(student)} {student.ToString()}");
        }

        public void Sort()
        {
            try
            {
                Console.Clear();
                Console.WriteLine(StudentsSort);
                switch (Console.ReadLine())
                {
                    case "1":
                        List<StudentDTO> sortByFirstName = studentService.SortByFirstName(StudentFile);
                        foreach (StudentDTO student in sortByFirstName)
                            Console.WriteLine($"{sortByFirstName.IndexOf(student)} {student.ToString()}");
                        break;
                    case "2":
                        List<StudentDTO> sortByLastName = studentService.SortByLastName(StudentFile);
                        foreach (StudentDTO student in sortByLastName)
                            Console.WriteLine($"{sortByLastName.IndexOf(student)} {student.ToString()}");
                        break;
                    case "3":
                        List<StudentDTO> sortByGroup = studentService.SortByGroup(StudentFile);
                        foreach (StudentDTO student in sortByGroup)
                            Console.WriteLine($"{sortByGroup.IndexOf(student)} {student.ToString()}");
                        break;
                    default:
                        Console.WriteLine("Enter sort number [1-3]");
                        Sort();
                        break;
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        public void Update()
        {
            try
            {
                Console.Clear();
                GetAll();
                int ID = GetStudentID();
                Console.WriteLine("Enter new student data");
                StudentDTO student = CommandConsole.AddStudent();
                ReaderFormDTO formDTO = new ReaderFormDTO
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ReaderID = student.StudentID,
                    BookCount = 0,
                    Books = new List<BookDTO>()
                };
                studentService.Update(ID, student, StudentFile);
                formService.Update(ID, formDTO, ReaderFile);
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
            int ID = GetStudentID();
            StudentDTO student = studentService.GetByID(ID, StudentFile);
            ReaderFormDTO reader = formService.GetByID(ID, ReaderFile);
            Console.WriteLine(student.ToString() + "\n" + reader.ToString());
            foreach (BookDTO book in reader.Books)
                Console.WriteLine($"{reader.Books.IndexOf(book)} {book.ToString()}");
        }

        //private StudentDTO View()
        //{
        //    Console.Clear();
        //    GetAll();
        //    int ID = GetStudentID();
        //    StudentDTO student = studentService.GetByID(ID, StudentFile);
        //    return student;
        //}

        public void TakeBook()
        {
            try
            {
                Console.Clear();
                List<BookDTO> books = bookService.GetAll(BookFile);
                foreach (BookDTO book in books)
                    Console.WriteLine($"{books.IndexOf(book)} {book.ToString()}");
                int ID = GetBookID();
                GetAll();
                int studID = GetStudentID();
                formService.TakeBook(ID, Form, BookFile, studID, ReaderFile);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully");
                Console.ResetColor();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        public void GetAllForms()
        {
            Console.Clear();
            List<ReaderFormDTO> readers = formService.GetAll(ReaderFile);
            foreach (ReaderFormDTO reader in readers)
                Console.WriteLine($"{readers.IndexOf(reader)} {reader.ToString()}");
        }

        public void ReturnBook()
        {
            try
            {
                Console.Clear();
                List<BookDTO> booksInForm = Form.Books;
                List<BookDTO> books = bookService.GetAll(BookFile);
                GetAllForms();
                int IDForm = GetStudentID();
                Console.WriteLine("Books in form");
                foreach (BookDTO book in booksInForm)
                    Console.WriteLine($"{booksInForm.IndexOf(book)} {book.ToString()}");
                int IDInForm = GetBookID();
                Console.WriteLine("Books in library");
                foreach (BookDTO book in books)
                    Console.WriteLine($"{books.IndexOf(book)} {book.ToString()}");
                int IDInLibrary = GetBookID();
                formService.ReturnBook(IDInForm, Form, ReaderFile, IDForm, BookFile, IDInLibrary);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully");
                Console.ResetColor();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
        }

        private int GetStudentID()
        {
            bool Parsing = false;
            int ID;
            do
            {
                Console.WriteLine("Select student");
                Parsing = int.TryParse(Console.ReadLine(), out ID);
                Console.WriteLine(Parsing ? "Waiting..." : "Enter student number [1-...]");
            } while (!Parsing);
            Console.Clear();
            return ID;
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

        private void SearchStudent()
        {
            Console.Clear();
            Console.WriteLine("Write keyword for book search");
            string key = Console.ReadLine();
            List<StudentDTO> books = studentService.SearchStudent(StudentFile, key);

            foreach (StudentDTO book in books)
                Console.WriteLine($"{books.IndexOf(book)} {book.ToString()}");
        }

        private readonly static string EnterFile = "Please, select the serialize type with which you want to work";
        private readonly static string MainCommands = "Available commands:\n1. Add new account\n2. Delete account\n3. Get a list of students" +
            "\n4. Sort the list of students\n5. View student reader form\n6. Change student data\n7. Take book" +
            "\n8. Return book\n9. Search student\n0. Back to welcome page";
        private readonly static string ErrorCheckIn = "Sorry, your ID was not found. Please try to register again";
        private readonly static string StudentsSort = "Select sort type:\n1. Sort by first name\n2. Sort by last name\n3. Sort" +
            "by group";
    }
}
