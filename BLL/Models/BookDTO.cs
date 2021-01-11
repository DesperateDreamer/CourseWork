using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsInLibrary { get; set; }
        //public int Year { get; set; }

        public override string ToString()
        {
            string Availability = IsInLibrary ? "Yes" : "No";
            return $"{Title}\nAuthor: {Author}\nAvailability: {Availability}";
        }
    }
}
