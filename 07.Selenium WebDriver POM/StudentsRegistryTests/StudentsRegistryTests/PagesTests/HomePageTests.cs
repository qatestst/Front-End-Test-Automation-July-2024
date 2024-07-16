using StudentsRegistryTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.PagesTests
{
    public class HomePageTests : BaseTest
    {
        [Test]
        public void TestHomePageContent()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();
            Assert.Multiple(() =>
                {
                    Assert.That(homePage.GetPageTitle(), Is.EqualTo("MVC Example"));
                    Assert.That(homePage.GetPageHeading, Is.EqualTo("Students Registry"));
                });

                Assert.True( homePage.StudensCount() > 0 );
        }


        [Test]
        public void TestHomePageLinks()
        {
            HomePage homePage = new HomePage(driver);
            homePage.OpenPage();

            homePage.HomeLink.Click();
            Assert.That(homePage.IsPageOpen, Is.True);

            homePage.OpenPage();
            homePage.ViewStudentsLink.Click();
            Assert.That(new ViewStudentsPage(driver).IsPageOpen, Is.True);

            homePage.OpenPage();
            homePage.AddStudentsLink.Click();
            Assert.That(new AddStudentPage(driver).IsPageOpen, Is.True);
        }


    }
}
