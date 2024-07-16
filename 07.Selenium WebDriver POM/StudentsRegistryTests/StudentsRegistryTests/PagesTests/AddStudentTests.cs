using OpenQA.Selenium;
using StudentsRegistryTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.PagesTests
{
    public class AddStudentTests : BaseTest
    {
        public AddStudentTests(IWebDriver driver): base()
        {            
        }

        [Test]
        public void TestAddStudentPage()
        {
            AddStudentPage addStudentsPage = new AddStudentPage(driver);
            addStudentsPage.OpenPage();
            Assert.Multiple(() =>
            {
                Assert.That(addStudentsPage.GetPageTitle(), Is.EqualTo("Add Student"));
                Assert.That(addStudentsPage.GetPageHeading, Is.EqualTo("Register New Student"));
            });

            Assert.That(addStudentsPage.FieldName.Text, Is.EqualTo(""));
            Assert.That(addStudentsPage.FieldEmail.Text, Is.EqualTo(""));
            Assert.That(addStudentsPage.ButtonSubmit.Text, Is.EqualTo("Add"));
        }

        
        [Test]
        public void TestAddStudentPageLinks()
        {
           // TO DO     .....        
        }

        // Create method to generate random name and return data as a string
        private string GenerateRandomName()
        {
            Random random = new Random();
            string[] names = { "Ivan", "Petar", "Todor", "Alex" };
            return names[random.Next(names.Length - 1)] + random.Next(999, 9999);
        }

        // Create method to generate random email and return data as a string
        private string GenerateRandomEmail(string name)
        {
            Random random = new Random();

            return name.ToLower() + random.Next(999, 9999).ToString() + "@gmail.com";
        }

        [Test]
        public void TestAddStudentPage_AddValidStudent()
        {
            AddStudentPage addStudentsPage = new AddStudentPage(driver);
            addStudentsPage.OpenPage();
            
            string name = GenerateRandomName();
            string email = GenerateRandomEmail(name);

            addStudentsPage.AddStudent(name, email);

            ViewStudentsPage viewStudentsPage = new ViewStudentsPage(driver);
            
            Assert.That(viewStudentsPage.IsPageOpen, Is.True);

            var students = viewStudentsPage.GetRegisterStudents();

            string newStudentFullSting = name + " (" + email + ")";
            Assert.True(students.Contains(newStudentFullSting));
                       
        }

        [Test]
        public void TestAddStudentPage_AddInvalidStudent()
        {
            AddStudentPage addStudentsPage = new AddStudentPage(driver);
            addStudentsPage.OpenPage();

            addStudentsPage.AddStudent("", "invalidData");

            
            Assert.That(addStudentsPage.IsPageOpen, Is.True);
            Assert.That(addStudentsPage.GetErrorMessage, Is.EqualTo("Cannot add student. Name and email fields are required!"));

        }


    }
}
