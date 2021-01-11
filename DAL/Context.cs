using System.Collections.Generic;
using DAL.Entities;

namespace DAL
{
    public static class Context
    {
        public static List<Student> Students { get; set; }
        public static List<ReaderForm> ReaderForms { get; set; }
        public static List<Book> Books { get; set; }
    }
}
