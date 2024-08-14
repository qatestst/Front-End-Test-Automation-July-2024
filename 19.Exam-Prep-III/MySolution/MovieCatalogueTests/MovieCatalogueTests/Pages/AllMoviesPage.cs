using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogueTests.Pages
{
    public class AllMoviesPage : BasePage
    {

        public AllMoviesPage(IWebDriver driver, Actions actions) : base(driver, actions)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/All";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement PosterUrlInput => driver.FindElement(By.XPath("//input[@name='PosterUrl']"));
        public IWebElement TrailerLinkInput => driver.FindElement(By.XPath("//input[@name='TrailerLink']"));
        public IWebElement MarkedAsWatchedCheckbox => driver.FindElement(By.XPath("//input[@name='IsWatched']"));
        public IWebElement EditButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));
        
        public IWebElement EditMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));


        public IWebElement LastPage => driver.FindElements(By.XPath("//ul[@class='pagination']//li")).Last();
        public IWebElement LastMovieTitle => driver.FindElements(By.XPath("//div[@class='col-lg-4']//h2")).Last();
        public IWebElement LastMovieEditButton => driver.FindElements(By.XPath("//a[@class='btn btn-outline-success']")).Last();
        public IWebElement LastMovieDeleteButton => driver.FindElements(By.XPath("//a[@class='btn btn-danger']")).Last();
        public IWebElement LastMovieDeleteConfirmButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));



        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public string GetLastMovieTitle()
        {
            driver.Navigate().GoToUrl(Url);
            LastPage.Click();

            return LastMovieTitle.Text;
        }

        public void EditLastCreatedMovie(string title, string description)
        {
            OpenPage();
            LastPage.Click();
            LastMovieEditButton.Click();

            TitleInput.Clear();
            TitleInput.SendKeys(title);

            DescriptionInput.Clear();
            DescriptionInput.SendKeys(description);

            actions.ScrollToElement(EditButton).Perform();

            EditButton.Click();
        }

        public void AssertSuccessfullyEditedMovieMessage()
        {
            Assert.That(EditMessage.Text.Trim(), Is.EqualTo("The Movie is edited successfully!"), "The message is not expected");
        }

        public void MarkMovieAsWatched()
        {
            OpenPage();
            LastPage.Click();
            LastMovieEditButton.Click();
            actions.ScrollToElement(EditButton).Perform();
            MarkedAsWatchedCheckbox.Click();

            
            EditButton.Click();
        }

        public void DeleteLastAddedMovie()
        {
            OpenPage();
            LastPage.Click();
            LastMovieDeleteButton.Click();
            LastMovieDeleteConfirmButton.Click();

        }

        public void AssertSuccessfullyDeletedMovieMessage()
        {
            Assert.That(EditMessage.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"), "The message is not expected");
        }
        



    }
}
