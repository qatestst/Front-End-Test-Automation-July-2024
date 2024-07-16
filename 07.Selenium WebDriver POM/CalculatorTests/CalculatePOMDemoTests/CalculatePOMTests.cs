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
            Assert.AreEqual(calculatorPage.ResultDiv.Text, "Sum: 3");

            calculatorPage.ResetForm();
            Assert.IsTrue(calculatorPage.IsFormEmpty());
        }

        [Test]
        public void AddTwoNumbers_ValidInput()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();
            
            string result = calculatorPage.AddNumbers("1", "2");

            // Assert
            Assert.AreEqual("Sum: 3", result);
               
        }

        [Test]
        public void Test3_RandomGeneratedNumbers_ValidInput()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();

            for (int i = 0; i < 10; i++) 
            {
                Random rnd = new Random();
                int num1 = ((short)rnd.Next());
                int num2 = ((short)rnd.Next());

                string number1 = num1.ToString();
                string number2 = num2.ToString();
                string res = (num1 + num2).ToString();

                string result = calculatorPage.AddNumbers(number1, number2);
                // Assert
                Assert.AreEqual($"Sum: "+res, result);
                Console.WriteLine($"{num1} + {num2} = {result}");

                //reset form
                calculatorPage.ResetForm();
            }
        }

        [Test]
        public void AddTwoNumbers_InvalidInput()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();

            string result = calculatorPage.AddNumbers("invalid", "number");

            // Assert
            Assert.AreEqual("Sum: invalid input", result);

        }

        [Test]

        public void Test_FormReset()
        {
            //Arrange
            var calculatorPage = new SumNumbersPage(driver);
            calculatorPage.OpenPage();

            string result = calculatorPage.AddNumbers("1", "2");

            Assert.AreEqual("Sum: 3", result);

            calculatorPage.ResetForm();
            Assert.True(calculatorPage.IsFormEmpty());

        }

    }
}