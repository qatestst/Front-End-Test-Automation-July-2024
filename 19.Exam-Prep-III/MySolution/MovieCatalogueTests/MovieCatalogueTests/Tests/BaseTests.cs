using MovieCatalogueTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace MovieCatalogueTests.Tests
{

    public class BaseTests
    {
        public IWebDriver driver;
        public Actions actions;

        public LoginPage loginPage;
        public AddMoviePage addMoviePage;
        public AllMoviesPage allMoviesPage;
        public WatchedMoviesPage watchedMoviesPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enebled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");


            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            
            loginPage = new LoginPage(driver, actions);
            loginPage.OpenPage();
            loginPage.PerformLogin("randomUser999@randomMail.com", "123456");

            addMoviePage = new AddMoviePage(driver, actions);
            allMoviesPage = new AllMoviesPage(driver, actions);
            watchedMoviesPage = new WatchedMoviesPage(driver, actions);
        }

        [OneTimeTearDown]
        public void TwoTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }



    }
}
