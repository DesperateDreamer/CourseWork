using System;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IStudentService
    {
        void Add(StudentDTO student, string nameOfFile);
        void Delete(int ID, string nameOfFile);
        List<StudentDTO> SortByFirstName(string nameOfFile);
        List<StudentDTO> SortByLastName(string nameOfFile);
        List<StudentDTO> SortByGroup(string nameOfFile);
        void Update(int ID, StudentDTO student, string nameOfFile);
        StudentDTO GetByID(int ID, string nameOfFile);
        List<StudentDTO> GetAll(string nameOfFile);
        List<StudentDTO> SearchStudent(string nameOfFile, string key);
    }
}
