using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogueTests.Pages
{
    public class WatchedMoviesPage : BasePage
    {

        public WatchedMoviesPage(IWebDriver driver, Actions actions) : base(driver, actions)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/Watched";

        public IWebElement LastPage => driver.FindElements(By.XPath("//ul[@class='pagination']//li")).Last();
        public IWebElement LastMovieTitle => driver.FindElements(By.XPath("//div[@class='col-lg-4']//h2")).Last();

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public string GetLastWatchedMovieTitle()
        {
            OpenPage();
            LastPage.Click();

            return LastMovieTitle.Text.Trim();
        }


    }
}
