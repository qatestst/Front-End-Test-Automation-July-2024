using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevueCraftersTests.Pages
{
    public class CreateRevuePage : BasePage
    {
        public CreateRevuePage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Revue/Create";


        public IWebElement TitleInputField => driver.FindElement(By.Id("form3Example1c"));
        public IWebElement ImageUrlInputField => driver.FindElement(By.Id("form3Example3c"));
        public IWebElement DescriptionInputField => driver.FindElement(By.Id("form3Example4cd"));
        public IWebElement CreateButtonCreateRevueForm => driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement ErrorMessageCreateRevue => driver.FindElement(By.XPath("//*[@id=\"createRevue\"]/div/div/div/div/div/div/div/div/ul/li"));
        
        public void CreateNewRevue(string title, string description)
        {
            driver.Navigate().GoToUrl(Url);
            actions.ScrollToElement(CreateButtonCreateRevueForm).Perform();

            TitleInputField.Clear();
            TitleInputField.SendKeys(title);

            DescriptionInputField.Clear();
            DescriptionInputField.SendKeys(description);

            CreateButtonCreateRevueForm.Click();
        }


    }
}
