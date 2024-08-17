using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class WatchedMoviesPage : BasePage
    {
        public WatchedMoviesPage(IWebDriver driver) : base(driver)
        {
        }

        public string Url = BaseUrl + "/Catalog/Watched";

        public IWebElement LastWatchedMoviesPage => driver.FindElements(By.XPath("//a[@class='page-link']")).Last();
        public IWebElement LastWatchedMovieTitle => driver.FindElements(By.XPath("//div[@class='col-lg-4']//h2")).Last();


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public string GetLastWatchedMovieTitle()
        {
            OpenPage();
            LastWatchedMoviesPage.Click();
            return LastWatchedMovieTitle.Text.Trim();

        }
    }
}
