using System;
using System.Collections.Generic;
using System.Text;
using BLL.Models;
using BLL.Services.EntityServices;
using DAL;
using NUnit.Framework;

namespace TestBLL
{
    public class StudentTests
    {
        string prov = "students_tests.bin";
        StudentService service = new StudentService("students_tests.bin");
        StudentService serviceTestJson = new StudentService("empty_students_tests.json");
        StudentService serviceTestXml = new StudentService("empty_students_tests.xml");

        [Test]
        public void Test_BookAdding()
        {
            //Arrange
            StudentDTO student = new StudentDTO
            {
                FirstName = "First",
                LastName = "Last",
                StudentID = "AA12345678",
                Group = 300,

            };

            //Act
            service.Add(student, prov);

            //Assert
            Assert.AreEqual(student.FirstName, Context.Students[0].FirstName);
        }

        [Test]
        public void Test_BookGetAll()
        {
            //Arrange
            StudentDTO student1 = new StudentDTO
            {
                FirstName = "First1",
                LastName = "Last",
                StudentID = "AA12345678",
                Group = 300,
            };
            service.Add(student1, prov);

            //Act
            List<StudentDTO> act = service.GetAll(prov);

            //Assert
            Assert.AreEqual(act[1].FirstName, Context.Students[1].FirstName);
        }

        [Test]
        public void Test_BookGetByID()
        {
            //Arrange
            StudentDTO student2 = new StudentDTO
            {
                FirstName = "First2",
                LastName = "Last",
                StudentID = "AA12345678",
                Group = 300,
            };
            service.Add(student2, prov);

            //Act
            StudentDTO act = service.GetByID(2, prov);

            //Assert
            Assert.AreEqual(student2.FirstName, Context.Students[2].FirstName);
        }

        [Test]
        public void Test_BookSortByFirstName()
        {
            //Arrange
            StudentDTO exp = new StudentDTO
            {
                FirstName = "Aaa",
                LastName = "Last",
                StudentID = "AA12345678",
                Group = 300,
            };
            service.Add(exp, prov);

            //Act
            List<StudentDTO> act = service.SortByFirstName(prov);

            //Assert
            Assert.AreEqual(exp.LastName, act[0].LastName);
        }

        public void Test_BookSortByLastName()
        {
            //Arrange
            StudentDTO exp = new StudentDTO
            {
                FirstName = "Last",
                LastName = "Aaaa",
                StudentID = "AA12345678",
                Group = 300,
            };
            service.Add(exp, prov);

            //Act
            List<StudentDTO> act = service.SortByLastName(prov);

            //Assert
            Assert.AreEqual(exp.Group, act[0].Group);
        }

        [Test]
        public void Test_BookSortByGroup()
        {
            //Arrange
            StudentDTO exp = new StudentDTO
            {
                FirstName = "Last",
                LastName = "Aaaa",
                StudentID = "AA12345678",
                Group = 101,
            };
            service.Add(exp, prov);

            //Act
            List<StudentDTO> act = service.SortByGroup(prov);

            //Assert
            Assert.AreEqual(exp.FirstName, act[0].FirstName);
        }

        [Test]
        public void Test_BookUpdating()
        {
            //Arrange
            StudentDTO student = new StudentDTO
            {
                FirstName = "Last",
                LastName = "Aaaa",
                StudentID = "AA12345678",
                Group = 101,
            };
            StudentDTO studentToUpdate = new StudentDTO
            {
                FirstName = "FirstUpdate",
                LastName = "LastUpdate",
                StudentID = "AA12345678",
                Group = 101,
            };
            service.Add(student, prov);

            //Act
            service.Update(Context.Students.Count - 1, studentToUpdate, prov);

            //Assert
            Assert.AreEqual(studentToUpdate.FirstName,
                Context.Students[Context.Students.Count - 1].FirstName);
            Assert.AreEqual(studentToUpdate.FirstName,
                Context.Students[Context.Students.Count - 1].FirstName);
        }

        [Test]
        public void Test_BookDeleting()
        {
            //Arrange
            StudentDTO studentDelete = new StudentDTO
            {
                FirstName = "FirstDelete",
                LastName = "LastDelete",
                StudentID = "AA12345678",
                Group = 101,
            };
            service.Add(studentDelete, prov);
            int actCount = Context.Students.Count;

            //Act
            service.Delete(Context.Students.Count - 1, prov);

            //Assert
            Assert.AreEqual(actCount - 1, Context.Students.Count);
        }

        [Test]
        public void Test_StudentSearch()
        {
            //Arrange
            List<StudentDTO> exp = new List<StudentDTO>();
            exp.Add(new StudentDTO
            {
                FirstName = "First",
                LastName = "Last",
                StudentID = "AA12345678",
                Group = 101,
            });

            //Act
            List<StudentDTO> act = service.SearchStudent(prov, "Last");

            //Assert
            Assert.AreEqual(exp[0].FirstName, act[0].FirstName);
        }
    }
}
