using OpenQA.Selenium;
using StudentsRegistryTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.PagesTests
{
    public class ViewStudentsTests : BaseTest
    {
        public ViewStudentsTests(IWebDriver driver) : base()
        {            
        }

        [Test]
        public void TestViewStudentsPageContent()
        {
            ViewStudentsPage viewStudentsPage = new ViewStudentsPage(driver);
            viewStudentsPage.OpenPage();
            Assert.Multiple(() =>
            {
                Assert.That(viewStudentsPage.GetPageTitle(), Is.EqualTo("Students"));
                Assert.That(viewStudentsPage.GetPageHeading, Is.EqualTo("Registered Students"));
            });

            var students = viewStudentsPage.GetRegisterStudents();

            foreach (var student in students)
            {
                Assert.That(student.Contains("("), Is.True);
                Assert.That(student.LastIndexOf(")") == student.Length-1, Is.True);
            }
        }


    }
}
