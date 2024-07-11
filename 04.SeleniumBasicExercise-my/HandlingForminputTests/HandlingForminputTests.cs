using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HandlingForminputTests
{
    public class Tests
    {
        WebDriver driver;

        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }



        [Test]
        public void HandlingFormInputs()
        {
            //click on [My account] button
            var myAccountButton = driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2];
            myAccountButton.Click();

            //click on [Continue] button
            driver.FindElement(By.LinkText("Continue")).Click();

            //Click on [Male] radio button
            driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

            //fill value in first name field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='firstname']")).SendKeys("FirstExampleName");

            //fill value in last name field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("LastExampleName");

            driver.FindElement(By.Id("dob")).SendKeys("05/06/1990");

            //fill email field
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string email = "testEmail" + randomNumber.ToString() + "@gmail.com";

            driver.FindElement(By.XPath("//input[@name='email_address']")).SendKeys(email);

            //fill company field
            driver.FindElement(By.XPath("//input[@name='company']")).SendKeys("TestCompany");

            //fill adress fields
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']")).SendKeys("street Adress Num.17");
            driver.FindElement(By.XPath("//input[@name='suburb']")).SendKeys("Test Suburb");
            driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("150034");
            driver.FindElement(By.XPath("//input[@name='city']")).SendKeys("Chicago");
            driver.FindElement(By.XPath("//input[@name='street_address']")).SendKeys("street Adress Num.17");
            driver.FindElement(By.XPath("//input[@name='state']")).SendKeys("BG");

            //select from country dropdown menu
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            //fill telephone number field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='telephone']")).SendKeys("0811999111");

            //fill fax number field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='fax']")).SendKeys("0003336654");

            //click on newsletter checkbox
            driver.FindElement(By.XPath("//input[@name='newsletter']")).Click();

            //fill password field
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='password']")).SendKeys("123456");
            //fill confirm password
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='confirmation']")).SendKeys("123456");

            //click on continue button
            driver.FindElements(By.XPath("//span[@class='ui-button-icon-primary ui-icon ui-icon-person']//following-sibling::span"))[1].Click();

            //Assert message for success

            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!");

            //click log off button
            driver.FindElement(By.LinkText("Log Off")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Created Successfully");
        }
    }
}