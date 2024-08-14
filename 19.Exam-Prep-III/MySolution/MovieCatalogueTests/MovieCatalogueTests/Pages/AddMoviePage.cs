using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogueTests.Pages
{
    public class AddMoviePage : BasePage
    {
        public AddMoviePage(IWebDriver driver, Actions actions) : base(driver, actions)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/Add";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement PosterUrlInput => driver.FindElement(By.XPath("//input[@name='PosterUrl']"));
        public IWebElement TrailerLinkInput => driver.FindElement(By.XPath("//input[@name='TrailerLink']"));
        public IWebElement MarkedAsWatchedCheckbox => driver.FindElement(By.XPath("//input[@name='IsWatched']"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));
        public IWebElement ToastMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));


        


        public void AssertEmptyTitleMessage()
        {
            Assert.That(ToastMessage.Text.Trim(), Is.EqualTo("The Title field is required."), "Title error message is not expected!");
        }

        public void AssertEmptyDescriptionMessage()
        {
            Assert.That(ToastMessage.Text.Trim(), Is.EqualTo("The Description field is required."), "Description error message is not expected!");
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void CreateMovie(string title, string description)
        {
            TitleInput.Clear();
            TitleInput.SendKeys(title);

            DescriptionInput.Clear();
            DescriptionInput.SendKeys(description);

            actions.ScrollToElement(AddButton).Perform();

            AddButton.Click();
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
