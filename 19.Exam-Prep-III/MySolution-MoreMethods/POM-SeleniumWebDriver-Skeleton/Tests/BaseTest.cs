using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using POM_SeleniumWebDriver_Skeleton.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Tests
{

    public class BaseTest
    {
        public IWebDriver driver;
        public IWebElement element;
        public Actions actions;


        public LoginPage loginPage;
        public HomePage homePage;
        public AddMoviePage addMoviePage;
        public AllMoviesPage allMoviesPage;
        public WatchedMoviesPage watchedMoviesPage;


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
            addMoviePage = new AddMoviePage(driver);
            allMoviesPage = new AllMoviesPage(driver);
            watchedMoviesPage = new WatchedMoviesPage(driver);

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        //Generate Methods
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

        public string GenerateRandomString(int stringLength)
        {
            Random rd = new Random();
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

    }
}
