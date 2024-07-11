using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CalculatorProject
{
    public class Tests
    {
        WebDriver driver;

        IWebElement textBoxNumber1;
        IWebElement textBoxNumber2;
        IWebElement dropdownOperation;
        IWebElement calcButton;
        IWebElement resetButton;
        IWebElement divResult;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [SetUp]
        public void Setup()
        {
           

            textBoxNumber1 = driver.FindElement(By.Id("number1"));
            textBoxNumber2 = driver.FindElement(By.XPath("//input[@id='number2']"));
            dropdownOperation = driver.FindElement(By.XPath("//label[@for='operation']//following-sibling::select"));
            calcButton = driver.FindElement(By.XPath("//input[@type='button']"));
            resetButton = driver.FindElement(By.XPath("//input[@type='Reset']"));
            divResult = driver.FindElement(By.XPath("//div[@id='result']"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void PerformTestLogic(string firstNumber, string secondNumber, string operation, string expected)
        {
            //click reset
            resetButton.Click();

            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxNumber1.SendKeys(firstNumber);
            }
            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxNumber2.SendKeys(secondNumber);
            }
            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropdownOperation).SelectByText(operation);
            }
            calcButton.Click();
            Assert.That(divResult.Text, Is.EqualTo(expected));
        }

        [Test]
        [TestCase( "5", "+ (sum)", "10", "Result: 15")]
        [TestCase( "5", "+ (sum)", "5", "Result: 10")]
        [TestCase( "15", "+ (sum)", "10", "Result: 25")]
        public void Test(string firstNumber, string operation, string secondNumber, string expected)
        {
            PerformTestLogic(firstNumber, secondNumber, operation, expected);
        }
    }
}