using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ReaderFormDTO
    {
        public int BookCount { get; set; }
        public List<BookDTO> Books { get; set; }
        public string ReaderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ReaderFormDTO()
        {
            BookCount = 0;
            Books = new List<BookDTO>();
        }

        public override string ToString()
        {
            return $"-_-_-Reader form of {FirstName} {LastName}-_-_-\nReaderID: {ReaderID}\nBookCount: {BookCount}";
        }
    }
}
