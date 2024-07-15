using CalculatePOM;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatePOMDemoTests
{
    public class CalculatePOMTests
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown() 
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();
            calculatorPage.FieldNum1.SendKeys("1");
            calculatorPage.FieldNum2.SendKeys("2");

            //Act
            calculatorPage.CalcButton.Click();
            
            // Assert
            Assert.AreEqual(calculatorPage.ResultDiv.Text, "3");

            calculatorPage.ResetForm();
            Assert.IsTrue(calculatorPage.IsFormEmpty());
        }

        [Test]
        public void Test2()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();
            
            string result = calculatorPage.AddNumbers("1", "2");

            // Assert
            Assert.AreEqual("3", result);
                       
        }


    }
}