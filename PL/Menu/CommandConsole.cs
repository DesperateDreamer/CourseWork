using System;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Menu
{
    public class CommandConsole
    {
        public static StudentDTO AddStudent()
        {
            StudentDTO student = new StudentDTO
            {
                FirstName = InputLogic.InputName("first name"),
                LastName = InputLogic.InputName("last name"),
                StudentID = InputLogic.InputID("student ID"),
                Group = InputLogic.InputGroup()
            };
            return student;
        }

        public static BookDTO AddBook()
        {
            BookDTO book = new BookDTO
            {
                Title = InputLogic.InputTitle(),
                Author = InputLogic.InputAuthor(),
                IsInLibrary = true
            };
            return book;
        }

        private static ReaderFormDTO AddForm()
        {
            ReaderFormDTO form = new ReaderFormDTO
            {
                BookCount = 0,
                Books = new List<BookDTO>()
            };
            return form;
        }
    }
}
