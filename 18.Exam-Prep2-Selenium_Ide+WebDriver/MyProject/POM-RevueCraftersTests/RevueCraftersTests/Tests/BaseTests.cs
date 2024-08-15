using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using RevueCraftersTests.Pages;

namespace RevueCraftersTests.Tests
{
    public class BaseTests
    {
        public IWebDriver driver;
        public Actions actions;

        public LoginPage loginPage;
        public HomePage homePage;
        public CreateRevuePage createRevuePage;
        public MyRevuesPage myRevuePage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");
                    
           
            driver = new ChromeDriver(chromeOptions);
            actions = new Actions(driver);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            loginPage.Login();

            homePage = new HomePage(driver);
            createRevuePage = new CreateRevuePage(driver);
            myRevuePage = new MyRevuesPage(driver);


        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public string GenerateRandomTitle()
        {
            var random = new Random();
            return "TITLE: " + random.Next(10000, 100000);
        }

        public string GenerateRandomDescription()
        {
            var random = new Random();
            return "DESCRIPTION: " + random.Next(10000, 100000);
        }
    }
}