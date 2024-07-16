using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.Pages
{
    public class AddStudentPage : HomePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver) 
        {
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/add-student";

        public IWebElement FieldName => driver.FindElement(By.XPath("//input[@id='name']"));
        public IWebElement FieldEmail => driver.FindElement(By.XPath("//input[@id='email']"));
        public IWebElement ButtonSubmit => driver.FindElement(By.XPath("//button[@type='submit']"));

        public void AddStudent(string name, string email)
        {
            this.FieldName.SendKeys(name);
            this.FieldEmail.SendKeys(email);
            this.ButtonSubmit.Click();
        }

        public IWebElement ErrorMessage => driver.FindElement(By.CssSelector("body > div"));

        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
        }



    }
}
