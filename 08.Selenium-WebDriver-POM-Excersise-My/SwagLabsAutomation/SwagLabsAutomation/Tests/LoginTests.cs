using SwagLabsAutomation.Pages;

namespace SwagLabsAutomation.Tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        public void TestLoginWithValidCredentials()
        {
            Login("standard_user", "secret_sauce");

            
            Assert.That(inventoryPage.IsInventoryPageLoaded(), Is.True, "The inventory page is not loaded after success login"); 
        }

        [Test]
        public void TestLoginWithInvalidCredentials()
        {
            Login("invalid_user", "secret_sauce");

            
            string error = loginPage.GetErrorMessage();

            Assert.That(error.Contains("Username and password do not match any user in this service"), "Error message is not correct");
        }

        [Test]
        public void TestLoginWithLockedOutUser()
        {
            Login("locked_out_user", "secret_sauce");

            
            string error = loginPage.GetErrorMessage();

            Assert.That(error.Contains("Sorry, this user has been locked out."), "Error message is not correct");
        }


    }

}
