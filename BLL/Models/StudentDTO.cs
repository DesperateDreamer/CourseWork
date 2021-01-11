using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class StudentDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentID { get; set; }
        public int Group { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}\nStudentID: {StudentID}\nGroup: {Group}";
        }
    }
}
