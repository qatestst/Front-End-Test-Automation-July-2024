using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatePOM
{
    public class SumNumbersPage
    {
        private readonly IWebDriver driver;

        public SumNumbersPage(IWebDriver driver)
        {
            this.driver = driver;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        const string PageUrl = "https://6d0d7f3e-b737-48ac-9335-ede1f14a4568-00-xui3q6pd5i1k.janeway.replit.dev/";

        // Maop webpage elements 
        public IWebElement FieldNum1 => driver.FindElement(By.XPath("//*[@id=\"number1\"]"));
        public IWebElement FieldNum2 => driver.FindElement(By.XPath("//*[@id=\"number2\"]"));
        public IWebElement CalcButton => driver.FindElement(By.XPath("//*[@id=\"calcButton\"]"));
        public IWebElement ResetButton => driver.FindElement(By.XPath("//*[@id=\"resetButton\"]"));
        public IWebElement ResultDiv => driver.FindElement(By.XPath("//body//form//div[@id='result']/pre"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public void ResetForm()
        { 
            ResetButton.Click();
        }

        public bool IsFormEmpty()
        {
            return FieldNum1.Text + FieldNum2.Text == "";
        }

        public string AddNumbers(string number1, string number2)
        {
            FieldNum1.SendKeys(number1);
            FieldNum2.SendKeys(number2);
            CalcButton.Click();

            return ResultDiv.Text;
        }
        
    }
}
