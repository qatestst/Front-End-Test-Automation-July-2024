using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace MovieCatalogueTests.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected IWebElement element;

        protected Actions actions;
        protected WebDriverWait wait;

        protected static string BaseUrl = "http://moviecatalog-env.eba-ubyppecf.eu-north-1.elasticbeanstalk.com";

        public BasePage(IWebDriver driver, Actions actions)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement LoginLink => driver.FindElement(By.XPath("//a[text()='Login']"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//a[@id='loginBtn']"));
        public IWebElement AddMovieLink => driver.FindElement(By.XPath("//a[text()='Add Movie']"));
        public IWebElement AllMoviesLink => driver.FindElement(By.XPath("//a[text()='All Movies']"));
        public IWebElement WatchedMoviesLink => driver.FindElement(By.XPath("//a[text()='Watched Movies']"));
        public IWebElement UnwatchedMoviesLink => driver.FindElement(By.XPath("//a[text()='Unwatched Movies']"));
        public IWebElement LogoutHereButton => driver.FindElement(By.XPath("//button[text()='Logout']"));


    }
}
