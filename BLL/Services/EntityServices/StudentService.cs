using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;
using DAL.Entities;
using BLL.Models;
using BLL.Services.IServices;
using DAL.Providers;

namespace BLL.Services.EntityServices
{
    public class StudentService : IStudentService
    {
        StudentRepository repository;

        public StudentService(string UserInput)
        {
            repository = new StudentRepository(SelectProvider(UserInput));
        }

        public void Add(StudentDTO student, string nameOfFile)
        {
            repository.Add(Mapper.FromDTO(student), nameOfFile);
        }

        public void Delete(int ID, string nameOfFile)
        {
            repository.Delete(ID, nameOfFile);
        }

        public List<StudentDTO> GetAll(string nameOfFile)
        {
            return repository.GetAll(nameOfFile).Select(x => Mapper.ToDTO(x)).ToList();
        }

        public StudentDTO GetByID(int ID, string nameOfFile)
        {
            return Mapper.ToDTO(repository.GetByID(ID, nameOfFile));
        }

        public List<StudentDTO> SortByFirstName(string nameOfFile)
        {
            return GetAll(nameOfFile).OrderBy(x => x.FirstName).ToList();
        }

        public List<StudentDTO> SortByGroup(string nameOfFile)
        {
            return GetAll(nameOfFile).OrderBy(x => x.Group).ToList();
        }

        public List<StudentDTO> SortByLastName(string nameOfFile)
        {
            return GetAll(nameOfFile).OrderBy(x => x.LastName).ToList();
        }

        public void Update(int ID, StudentDTO student, string nameOfFile)
        {
            repository.Update(ID, Mapper.FromDTO(student), nameOfFile);
        }

        public List<StudentDTO> SearchStudent(string nameOfFile, string key)
        {
            return repository.GetAll(nameOfFile)
                .Where(x => x.FirstName.Contains(key)
                    || x.LastName.Contains(key)
                    || x.StudentID.Contains(key))
                .Select(x => Mapper.ToDTO(x))
                .ToList();
        }

        private IDataProvider<List<Student>> SelectProvider(string UserInput)
        {
            if (UserInput.EndsWith(".bin"))
                return new BinaryDataProvider<List<Student>>();
            else if (UserInput.EndsWith(".json"))
                return new JSONDataProvider<List<Student>>();
            else if (UserInput.EndsWith(".xml"))
                return new XMLDataProvider<List<Student>>();
            else
                throw new System.Exception();
        }

        public StudentDTO CheckIn(string UserID, string nameOfFile)
        {
            var CheckInStudents = GetAll(nameOfFile);
            foreach (StudentDTO student in CheckInStudents)
            {
                if (student.StudentID.Equals(UserID))
                    return student;
            }
            return null;
        }
    }
}
