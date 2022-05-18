using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Students;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetStudentWithNameAndMarks()
        {
            //Act
            Student ivanov = new Student { Name = "Petr Ivanov" };

            //Assert
            Assert.IsNotNull(ivanov);
            Assert.IsNotNull(ivanov.Name);
            Assert.IsTrue(ivanov.Name == "Petr Ivanov");
            Assert.IsNotNull(ivanov.Marks);
        }

        [TestMethod]
        public void AddMarkToStudent()
        {
            //Arrange
            Student ivanov = new Student { Name = "Petr Ivanov" };

            //Act
            ivanov.AddMark("Программирование", 2);

            //Assert
            Assert.IsTrue(ivanov.Marks.Count > 0);
            Assert.IsTrue(ivanov.Marks["Программирование"] == 2);
        }

        [TestMethod]
        public void ChangeMarkToStudent()
        {
            //Arrange
            Student ivanov = new Student { Name = "Petr Ivanov" };
            ivanov.AddMark("Программирование", 2);

            //Act
            ivanov.AddMark("Программирование", 5);

            //Assert
            Assert.IsTrue(ivanov.Marks.Count > 0);
            Assert.IsTrue(ivanov.Marks["Программирование"] == 5);
        }

        [TestMethod]
        public void CompareStudents()
        {
            //Arrange
            Student ivanov = new Student { Name = "Petr Ivanov" };
            Student petrov = new Student { Name = "Ivan Petrov" };
            List<Student> studentsList = new List<Student> {petrov, ivanov};

            //Act
            studentsList.Sort();

            //Assert
            Assert.IsTrue(studentsList[0].Name == "Petr Ivanov");
        }
    }
}
