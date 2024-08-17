using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class AddMoviePage : BasePage
    {
        public AddMoviePage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Catalog/Add";

        public IWebElement TitleInputAddMovieForm => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement DescriptionInputAddMovieForm => driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement MarkedAsWatchedCheckboxButtonAddMovieForm => driver.FindElement(By.XPath("//input[@name='IsWatched'][@type='checkbox']"));
        public IWebElement AddButtonAddMovieForm => driver.FindElement(By.XPath("//button[@class='btn warning'][@type='submit']"));

        // Error message-empty Title  is  "The Title field is required."
        // Error message-empty Description  is  "The Description field is required."

        public IWebElement ErrorMessageAddMovie => driver.FindElement(By.XPath("//div[@class='toast-message']")); 


        

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void AddMovie(string title, string description)
        {
            OpenPage();

            TitleInputAddMovieForm.Clear();
            TitleInputAddMovieForm.SendKeys(title);

            DescriptionInputAddMovieForm.Clear();
            DescriptionInputAddMovieForm.SendKeys(description);

            AddButtonAddMovieForm.Click();

        }

    }
}
