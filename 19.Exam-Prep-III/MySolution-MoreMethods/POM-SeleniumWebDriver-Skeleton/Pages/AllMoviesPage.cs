using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class AllMoviesPage : BasePage
    {
        public AllMoviesPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/All";

        public IWebElement LastPage => driver.FindElements(By.XPath("//a[@class='page-link']")).Last();
        public IWebElement LastMovieTitle => driver.FindElements(By.XPath("//div[@class='col-lg-4']//h2")).Last();


        // Edit IWebElements
        public IWebElement EditButtonLinkAllMoviesPage => driver.FindElements(By.XPath("//a[@class='btn btn-outline-success']")).Last();
        public IWebElement TitleInputEditMovieForm => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement DescriptionInputEditMovieForm => driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement MarkedAsWatchedCheckboxButtonEditMovieForm => driver.FindElement(By.XPath("//input[@name='IsWatched'][@type='checkbox']"));
        public IWebElement EditButtonEditMovieForm => driver.FindElement(By.XPath("//button[@class='btn warning'][@type='submit']"));

        public IWebElement MessageEditMovieForm => driver.FindElement(By.XPath("//div[@class='toast-message']")); // "The Movie is edited successfully!"

        //Delete IWebElements
        public IWebElement DeleteButtonLinkAllMoviesPage => driver.FindElements(By.XPath("//a[@class='btn btn-danger']")).Last();
        public IWebElement DeleteYesButtonDeletePage => driver.FindElement(By.XPath("//button[@class='btn warning']"));

        public IWebElement MessageDeletedSuccessfully => driver.FindElement(By.XPath("//div[@class='toast-message']")); // "The Movie is deleted successfully!"


        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }


        public void EditMovie(string title, string description)
        {
            OpenPage();
            LastPage.Click();
            EditButtonLinkAllMoviesPage.Click();
            
            TitleInputEditMovieForm.Clear();
            TitleInputEditMovieForm.SendKeys(title);

            DescriptionInputEditMovieForm.Clear();
            DescriptionInputEditMovieForm.SendKeys(description);

            EditButtonEditMovieForm.Click();

        }

        public void MarkAsWatchedLastAddedMovie()
        {
            OpenPage();
            LastPage.Click();
            EditButtonLinkAllMoviesPage.Click();
            MarkedAsWatchedCheckboxButtonEditMovieForm.Click();
            EditButtonEditMovieForm.Click();
        }

        public string GetLastAddedMovieTitle()
        {
            OpenPage();
            LastPage.Click();
            return LastMovieTitle.Text.Trim();
        }

        public void DeleteLastMovie()
        {
            OpenPage();
            LastPage.Click();
            DeleteButtonLinkAllMoviesPage.Click();
            DeleteYesButtonDeletePage.Click();
        }


    }
}
