using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class ReaderForm
    {
        private int bookCount;
        private List<Book> books;
        private string readerID;
        private string firstName;
        private string lastName;

        public int BookCount { get => bookCount; set => bookCount = value; }
        public List<Book> Books { get => books; set => books = value; }
        public string ReaderID { get => readerID; set => readerID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
    }
}
