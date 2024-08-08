using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaProjectTests.Pages
{
    public class IdeasEditPage : BasePage
    {
        public IdeasEditPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement ImageInput => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));

        public IWebElement EditButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));

    }
}
