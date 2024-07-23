using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumTestsExercise1
{
    public class Tests
    {
        private AndroidDriver _driver;

        //create local server
        private AppiumLocalService _appiumLocalService;



        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions()
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel7API",
                App = "D:\\SoftUni\\9 Front-End Test Automation - july 2024 - lections\\10.ApksFortesting\\com.example.androidappsummator.apk",
                PlatformVersion = "14",
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
            _appiumLocalService?.Dispose();
        }
                
        [Test]
        public void TestWithValidData()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("1");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("2");

            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            
            Assert.That(resultField.Text, Is.EqualTo("3"));
        }

        [Test]
        public void TestWithInvalidDataFillOnlyFirstField()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("1");

            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [Test]
        public void TestWithInvalidDataFillOnlySecondField()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys("2");

            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }


        [Test]
        public void TestWithInvalidDataFirstAndSecondField()
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys("e");

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys(".");

            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }

        [TestCase("10", "10", "20")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "1000", "1000")]
        [TestCase("10.9", "10.1", "21.0")]
        [TestCase("-10", "10", "0")]

        public void TestWithValidData_Parametrized(string input1, string input2, string expectedREsult)
        {
            IWebElement field1 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            field1.Clear();
            field1.SendKeys(input1);

            IWebElement field2 = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            field2.Clear();
            field2.SendKeys(input2);

            IWebElement calcButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            IWebElement resultField = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(resultField.Text, Is.EqualTo(expectedREsult));
        }


    }
}