using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Serializable]
    public class Student
    {
        private string firstName;
        private string lastName;
        private int group;

        private string studentID;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string StudentID { get => studentID; set => studentID = value; }
        public int Group { get => group; set => group = value; }
    }
}
