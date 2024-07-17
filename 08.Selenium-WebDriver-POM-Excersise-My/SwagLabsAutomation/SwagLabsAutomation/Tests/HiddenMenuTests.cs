using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Tests
{
    public class HiddenMenuTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
            
        }

        [Test]
        public void TestOpenMenu()
        {
            hiddenMenuPage.ClickHamburgerButton();
            Assert.True(hiddenMenuPage.IsMenuOpen(), "Hidden menu was not open");
        }

        [Test]
        public void TestLogout()
        {
            hiddenMenuPage.ClickHamburgerButton();
            hiddenMenuPage.ClickLogoutButton();
            Assert.True(driver.Url.Equals("https://www.saucedemo.com/"), "User was not logged out.");
        }

    }
}
